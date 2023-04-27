namespace ApplicationCore.Models.Common.Interfaces;

public interface IHasDateRange
{
    DateTime? DateFrom { get; set; }

    DateTime? DateTo { get; set; }
}