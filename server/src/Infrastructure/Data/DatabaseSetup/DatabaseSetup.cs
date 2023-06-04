using ApplicationCore.Constants;
using ApplicationCore.Entities;
using ApplicationCore.Entities.InitialData;
using ApplicationCore.Enums;
using ApplicationCore.Interfaces.Users;
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
    internal static readonly Guid ManagerUserId = Guid.Parse("1adfb348-1d0e-44db-8248-b221c057362e");
    internal static readonly Guid ManagerAssignmentId = Guid.Parse("49aaf3ca-11c0-4a2e-97c7-4bacea59f938");

    private readonly ApplicationDbContext _context;
    private readonly IOptions<InitialSetupSettings> _options;
    private readonly IUserPasswordService _userPasswordService;

    public DatabaseSetup(
        ApplicationDbContext context,
        IOptions<InitialSetupSettings> options,
        IUserPasswordService userPasswordService)
    {
        _context = context;
        _options = options;
        _userPasswordService = userPasswordService;
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
        var staffUser = new User
        {
            Id = ChiefOfStaffUserId,
            Email = "staff@gmail.com",
            PasswordHash = string.Empty,
            FirstName = "staff",
            LastName = "staff",
            CreatedAt = DateTime.UtcNow,
        };
        
        var managerUser = new User
        {
            Id = ManagerUserId,
            Email = "manager@gmail.com",
            PasswordHash = string.Empty,
            FirstName = "manager",
            LastName = "manager",
            CreatedAt = DateTime.UtcNow,
        };
        
        _userPasswordService.AssignUserPassword(staffUser, "Qwerty!123");
        _userPasswordService.AssignUserPassword(managerUser, "Qwerty!123");
        
        _context.Set<User>().AddRange(staffUser, managerUser);
    }

    private void SetupAssignments()
    {
        var staffAssignment = new Assignment
        {
            Id = ChiefOfStaffAssignmentId,
            UserId = ChiefOfStaffUserId,
            Status = AssignmentStatus.Active,
            CreatedAt = DateTime.UtcNow,
            CreatedByUserId = ChiefOfStaffUserId,
            Role = _context.Set<Role>().Single(r => r.Name == Roles.Staff),
            Phone = "+375",
        };
        
        var managerAssignment = new Assignment
        {
            Id = ManagerAssignmentId,
            UserId = ManagerUserId,
            Status = AssignmentStatus.Active,
            CreatedAt = DateTime.UtcNow,
            CreatedByUserId = ChiefOfStaffUserId,
            Role = _context.Set<Role>().Single(r => r.Name == Roles.Manager),
            Phone = "+375",
        };
        
        _context.Set<Assignment>().AddRange(staffAssignment, managerAssignment);
    }
}