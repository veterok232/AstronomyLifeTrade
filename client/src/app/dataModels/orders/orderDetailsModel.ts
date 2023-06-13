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
    customerEmail: string;
    customerPhoneNumber: string;
    totalAmount: number;
    quantity: number;
    orderStatus: OrderStatus;
    deliveryType: DeliveryType;
    paymentMethod: PaymentMethod;
    address?: Address;
    orderItems: OrderItem[];
    customerNotes?: string;
    managerFullName?: string;
    promoCode?: string;
    promoRate?: number;
    promoAmount?: number;
}