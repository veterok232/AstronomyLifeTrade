using ApplicationCore.Entities.Interfaces;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.AuthContext;
using ApplicationCore.Services.Dependencies.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Data;

[ScopedDependency]
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    private readonly IAuthContextAccessor _authContext;

    private static readonly Type[] CommonInterfaces =
    {
        typeof(IHasCreatedAt),
        typeof(IHasCreatedByUser),
        typeof(IHasCreatedByAssignment),
        typeof(IHasUpdatedAt),
        typeof(IHasModifiedByAssignment),
        typeof(IHasModifiedAt),
    };

    public UnitOfWork(ApplicationDbContext context, IAuthContextAccessor authContext)
    {
        _context = context;
        _authContext = authContext;
    }

    public Task Commit()
    {
        SetCommonProperties();
        UpdateVersion();
        return _context.SaveChangesAsync();
    }

    private void UpdateVersion()
    {
        _context.ChangeTracker.Entries<IHasVersion>()
            .Where(entry => entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
            .ForEach(entry => entry.OriginalValues[nameof(IHasVersion.Version)] = entry.Entity.Version);
        _context.ChangeTracker.Entries<IHasVersion>()
            .Where(entry => entry.State == EntityState.Modified || entry.State == EntityState.Added)
            .ForEach(entry => entry.Entity.Version = Guid.NewGuid());
    }

    private void SetCommonProperties()
    {
        foreach (var entity in GetAddedAndUpdatedEntities())
        {
            SetEntityOperationDateProperties(entity);
            SetEntityOperationPersonProperties(entity);
        }
    }

    private static void SetEntityOperationDateProperties(EntityEntry entity)
    {
        switch (entity.Entity)
        {
            case IHasCreatedAt createdEntity when entity.State == EntityState.Added:
                createdEntity.CreatedAt = DateTime.UtcNow;
                break;
            case IHasUpdatedAt updatedEntity when entity.State == EntityState.Modified:
                updatedEntity.UpdatedAt = DateTime.UtcNow;
                break;
            case IHasModifiedAt modifiedEntity
                when entity.State is EntityState.Added or EntityState.Modified:
                modifiedEntity.ModifiedAt = DateTime.UtcNow;
                break;
        }
    }

    private void SetEntityOperationPersonProperties(EntityEntry entity)
    {
        switch (entity.Entity)
        {
            case IHasCreatedByUser createdEntity when entity.State == EntityState.Added:
                createdEntity.CreatedByUserId = _authContext.UserId.Value;
                break;
            case IHasCreatedByAssignment createdEntity when entity.State == EntityState.Added:
                createdEntity.CreatedByAssignmentId = _authContext.AssignmentId.Value;
                break;
            case IHasModifiedByAssignment modifiedEntity
                when entity.State is EntityState.Added or EntityState.Modified:
                modifiedEntity.ModifiedByAssignmentId = _authContext.AssignmentId.Value;
                break;
        }
    }

    private IEnumerable<EntityEntry> GetAddedAndUpdatedEntities() => _context.ChangeTracker.Entries().Where(x =>
        x.Entity.GetType().GetInterfaces().Intersect(CommonInterfaces).Any() &&
        (x.State == EntityState.Added || x.State == EntityState.Modified));
}