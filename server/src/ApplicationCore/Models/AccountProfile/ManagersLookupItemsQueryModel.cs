namespace ApplicationCore.Models.AccountProfile;

public record ManagersLookupItemsQueryModel
{
    public string? SearchValue { get; init; }

    public Guid? SelectedId { get; init; }
}