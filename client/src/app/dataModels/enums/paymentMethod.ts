import { registerEnumForLocalization } from "../../components/localization/enumRegistrator";

export enum PaymentMethod
{
    Cash = 1,
    Card = 2,
}

registerEnumForLocalization({ PaymentMethod: PaymentMethod});