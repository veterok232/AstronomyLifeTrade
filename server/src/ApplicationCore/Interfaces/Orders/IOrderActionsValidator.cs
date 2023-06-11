using ApplicationCore.Entities;
using ApplicationCore.Models.Common;

namespace ApplicationCore.Interfaces.Orders;

public interface IOrderActionsValidator
{
    Result IsValidForApprove(Order order);
}