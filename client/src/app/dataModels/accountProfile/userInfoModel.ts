import { Address } from "../address";

export interface UserInfoModel {
    firstName: string;
    lastName: string;
    phone: string;
    email: string;
    address?: Address;
    birthday?: Date;
    gender?: string;
}