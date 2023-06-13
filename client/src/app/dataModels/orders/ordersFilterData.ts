import { OrderStatus } from "../enums/orderStatus";

export interface OrdersFilterData {
    orderStatuses?: OrderStatus[];
    orderNumber?: number;
    isWithoutManager?: boolean;
}