namespace ApplicationCore.Models.AccountProfile;

public record UsersLookupItemsQueryModel
{
    public string? SearchValue { get; init; }

    public Guid? SelectedId { get; init; }
}