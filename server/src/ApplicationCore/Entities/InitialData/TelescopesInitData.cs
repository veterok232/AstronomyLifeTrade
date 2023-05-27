using ApplicationCore.Enums;

namespace ApplicationCore.Entities.InitialData;

public static class TelescopesInitData
{
    public static readonly Telescope[] Data =
    {
        new Telescope
        {
            Id = Guid.Parse("9b9fbff4-3a3a-4a71-b1d7-8a09872919d7"),
            Aperture = 50,
            ApertureRatio = 1,
            EyepieceFittingDiameter = 1,
            FocusDistance = 600,
            MaxUsefulScale = 100,
            MinUsefulScale = 1,
            MountingType = MountingType.Azimutal,
            TelescopeControlType = TelescopeControlType.Manual,
            ScaleMax = 1,
            ScaleMin = 1,
            Seeker = "",
            TripodHeight = "",
            TripodMaterial = "",
            Type = TelescopeType.Refractor,
            TelescopeUserLevel = TelescopeUserLevel.ForBeginners,
            Weight = 20,
            ProductId = Guid.Parse("26e52bab-4c7e-4f92-b46e-8708b1e4f302"),
            CreatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
        },
        new Telescope
        {
            Id = Guid.Parse("9b9fbff4-3a3a-4a71-b1d7-1a69872919d7"),
            Aperture = 150,
            ApertureRatio = 9.3m,
            EyepieceFittingDiameter = 1.25m,
            FocusDistance = 1400,
            MaxUsefulScale = 1050,
            MinUsefulScale = 70,
            MountingType = MountingType.Equatorial,
            TelescopeControlType = TelescopeControlType.Manual,
            ScaleMax = 70,
            ScaleMin = 1050,
            Seeker = "с красной точкой",
            TripodHeight = "",
            TripodMaterial = "стальная",
            Type = TelescopeType.Reflector,
            TelescopeUserLevel = TelescopeUserLevel.ForConfidentUsers,
            Weight = null,
            ProductId = Guid.Parse("26e52bab-4c7e-4f92-b46e-8708b1936302"),
            CreatedAt = new DateTime(2023, 01, 03, 0, 0, 0, DateTimeKind.Utc),
        }
    };
}