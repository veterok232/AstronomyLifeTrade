import { Address } from "../address";
import { DeliveryType } from "../enums/deliveryType";
import { OrderStatus } from "../enums/orderStatus";
import { PaymentMethod } from "../enums/paymentMethod";
import { OrderItem } from "./orderItem";

export interface OrderDetailsModel {
    id: string;
    orderNumber: number;
    createdAt: Date;
    customerFirstName: string;
    customerLastName: string;
    totalAmount: number;
    quantity: number;
    orderStatus: OrderStatus;
    deliveryType: DeliveryType;
    paymentMethod: PaymentMethod;
    address?: Address;
    orderItems: OrderItem[];
}