using ApplicationCore.Constants;

namespace ApplicationCore.Entities.InitialData;

public static class UsersInitData
{
    public static readonly Guid SystemUserId = new Guid("8faeffed-e97c-4262-9bb9-995f558e6c8c");

    public static readonly User[] Data =
    {
        new User
        {
            Id = SystemUserId,
            Email = "systemEmail",
            FirstName = "System",
            LastName = string.Empty,
            PasswordHash = null,
            CreatedByUserId = SystemUserId,
            CreatedAt = new DateTime(2020, 11, 09, 0, 0, 0, DateTimeKind.Utc),
            Birthday = new DateOnly(2020, 11, 09).AddYears(-UserConstants.AdultAge),
        },
    };
}