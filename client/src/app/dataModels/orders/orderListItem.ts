import { OrderStatus } from "../enums/orderStatus";

export interface OrderListItem {
    id: string;
    status: OrderStatus;
    totalAmount: number;
    quantity: number;
    createdAt: Date;
    customerFirstName: string;
    customerLastName: string;
    managerFullName?: string;
    orderNumber: number;
}