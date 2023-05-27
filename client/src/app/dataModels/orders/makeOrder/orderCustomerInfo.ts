import { Address } from "../../address";

export interface OrderCustomerInfo {
    firstName: string;
    lastName: string;
    phone: string;
    email: string;
    address: Address;
}