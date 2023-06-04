import { registerEnumForLocalization } from "../../components/localization/enumRegistrator";

export enum DeliveryType {
    SelfPick = 1,
    Courier = 2,
}

registerEnumForLocalization({ DeliveryType: DeliveryType});