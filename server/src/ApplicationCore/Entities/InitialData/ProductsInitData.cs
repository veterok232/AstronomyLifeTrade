namespace ApplicationCore.Entities.InitialData;

public static class ProductsInitData
{
    public static readonly Product[] Data =
    {
        new Product()
        {
            Id = Guid.Parse("26e52bab-4c7e-4f92-b46e-8708b1e4f302"),
            BrandId = Guid.Parse("445faffb-0213-42f4-ab19-a22834ec3cb6"),
            CategoryId = Guid.Parse("df445d42-ca49-4fc5-9573-a14371daf34b"),
            Code = "T1",
            Price = 703.60m,
            Manufacturer = "КНР for Levenhuk, Inc. (USA)",
            Quantity = 20,
            Name = "Телескоп Levenhuk Skyline Travel Sun 70",
            ShortDescription = "компактный рефрактор для прогулок и путешествий.",
            SpecialNote = "",
            Description = "Телескоп Levenhuk Skyline Travel Sun 70 – компактный рефрактор для прогулок и путешествий. Длина его трубы составляет всего 40 см при апертуре 70 мм. В комплект входит удобный фирменный рюкзак, в него легко поместятся телескоп, монтировка, тренога и все необходимые аксессуары. Оптический прибор подойдет для наблюдения планет и спутников, а также деталей ландшафта и архитектуры. В идеальных условиях в него можно рассмотреть большинство объектов из каталога Мессье (без деталей), щель Кассини, кольца Сатурна и Большое Красное Пятно на Юпитере.",
            Equipment = "",
            CreatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            ModifiedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
            DeletedAt = DateTime.MinValue,
        }
    };
}