import { DeliveryType } from "../../enums/deliveryType";
import { PaymentMethod } from "../../enums/paymentMethod";
import { OrderCustomerInfo } from "./orderCustomerInfo";

export interface MakeOrderModel {
    cartItemsIds: string[];
    totalAmount: number;
    customerInfo: OrderCustomerInfo;
    deliveryType?: DeliveryType;
    paymentMethod?: PaymentMethod;
    customerNotes?: string;
    promocode?: string;
}