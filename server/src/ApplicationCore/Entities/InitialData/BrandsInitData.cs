using ApplicationCore.Enums;

namespace ApplicationCore.Entities.InitialData;

public static class BrandsInitData
{
    public static readonly Brand[] Data =
    {
        new Brand
        {
            Id = Guid.Parse("445faffb-0213-42f4-ab19-a22834ec3cb6"),
            Name = "Levenhuk",
            Code = "1",
            CategoryType = CategoryType.Telescopes,
        },
        new Brand
        {
            Id = Guid.Parse("5bfaa88a-8b4c-4092-8c41-f9c2e3982ff1"),
            Name = "Bresser",
            Code = "2",
            CategoryType = CategoryType.Telescopes,
        },
        new Brand
        {
            Id = Guid.Parse("1a73828e-952c-488d-b5d9-25309e33b619"),
            Name = "Sky-Watcher",
            Code = "3",
            CategoryType = CategoryType.Telescopes,
        },
        new Brand
        {
            Id = Guid.Parse("bd947615-ca7d-4db1-a6ec-f685f8f25876"),
            Name = "Levenhuk",
            Code = "4",
            CategoryType = CategoryType.Binoculars,
        },
        new Brand
        {
            Id = Guid.Parse("7a5776d8-914b-4bf1-a0be-c61467ccd3f6"),
            Name = "Bresser",
            Code = "5",
            CategoryType = CategoryType.Binoculars,
        },
    };
}