import { OrderStatus } from "../../../dataModels/enums/orderStatus";
import { EnumDictionary } from "../../../infrastructure/enums/enumDictionary";

export const OrderStatusColors: EnumDictionary<OrderStatus, string> = {
    [OrderStatus.Pending]: "#4ed6dc",
    [OrderStatus.Postponed]: "#fdac34",
    [OrderStatus.Cancelled]: "#555d93",
    [OrderStatus.Approved]: "#13ac6c",
    [OrderStatus.Closed]: "#90a2d3",
};
