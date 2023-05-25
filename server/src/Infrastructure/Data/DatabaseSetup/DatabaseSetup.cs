using ApplicationCore.Constants;
using ApplicationCore.Entities;
using ApplicationCore.Entities.InitialData;
using ApplicationCore.Enums;
using ApplicationCore.Services.Dependencies.Attributes;
using ApplicationCore.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Infrastructure.Data.DatabaseSetup;

[SelfScopedDependency]
public class DatabaseSetup
{
    internal static readonly Guid ChiefOfStaffUserId = Guid.Parse("7e2de431-a0db-4818-a0bc-ac2c5e04b4bb");
    internal static readonly Guid ChiefOfStaffAssignmentId = Guid.Parse("2b722edb-79b6-4b27-80d1-de0998851148");

    private readonly ApplicationDbContext _context;
    private readonly IOptions<InitialSetupSettings> _options;

    public DatabaseSetup(
        ApplicationDbContext context,
        IOptions<InitialSetupSettings> options)
    {
        _context = context;
        _options = options;
    }

    public void Run()
    {
        _context.Database.Migrate();
        ReloadDbTypes();

        if (NoDataExists())
        {
            using var transaction = _context.Database.BeginTransaction();
            SetupInitialData();
            _context.SaveChanges();
            transaction.Commit();
        }
    }

    // the first time Npgsql connects to a database, it loads all of that database's type information and caches that for all further connections
    // we need to reload the cache after applying migrations since the migrations may add some new types
    private void ReloadDbTypes()
    {
        if (_context.Database.GetDbConnection() is NpgsqlConnection npgsqlConnection)
        {
            npgsqlConnection.Open();
            npgsqlConnection.ReloadTypes();
        }
    }

    private bool NoDataExists()
    {
        return !_context.Set<User>().Any(u => u.Id != UsersInitData.SystemUserId);
    }

    private void SetupInitialData()
    {
        SetupUsers();
        SetupAssignments();
    }

    private void SetupUsers()
    {
        _context.Set<User>().AddRange(
            new User
            {
                Id = ChiefOfStaffUserId,
                Email = "veterok232@yandex.ru",
                PasswordHash = string.Empty,
                FirstName = "staff",
                LastName = "staff",
                CreatedAt = DateTime.UtcNow,
            });
    }

    private void SetupAssignments()
    {
        _context.Set<Assignment>().AddRange(
            new Assignment
            {
                Id = ChiefOfStaffAssignmentId,
                UserId = ChiefOfStaffUserId,
                Status = AssignmentStatus.Active,
                CreatedAt = DateTime.UtcNow,
                CreatedByUserId = ChiefOfStaffUserId,
                Role = _context.Set<Role>().Single(r => r.Name == Roles.Staff),
                Phone = "+1",
            });
    }
}