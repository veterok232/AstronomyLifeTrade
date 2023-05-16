namespace ApplicationCore.Entities.InitialData;

public static class CategoryInitData
{
    public static readonly Category[] Data =
    {
        new Category
        {
            Id = Guid.Parse("df445d42-ca49-4fc5-9573-a14371daf34b"),
            Name = "Telescopes",
            Code = "1",
            Description = "Telescopes"
        },
        new Category
        {
            Id = Guid.Parse("e3e6c141-8d10-49fb-8f60-a0393425d025"),
            Name = "Binoculars",
            Code = "2",
            Description = "Binoculars"
        },
        new Category
        {
            Id = Guid.Parse("6140552f-af4c-4d2b-8c35-41d764eb1ba3"),
            Name = "Accessories",
            Code = "3",
            Description = "Accessories"
        },
    };
}