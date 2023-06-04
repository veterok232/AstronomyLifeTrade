import { registerEnumForLocalization } from "../../components/localization/enumRegistrator";

export enum OrderStatus {
    Pending = 1,
    Postponed = 2,
    Cancelled = 3,
    Approved = 4,
    Closed = 5,
}

registerEnumForLocalization({ OrderStatus: OrderStatus });