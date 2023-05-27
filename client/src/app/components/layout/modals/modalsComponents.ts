import { EnumDictionary } from "../../../infrastructure/enums/enumDictionary";
import { MakeOrderConfirmationModal } from "../../orders/makeOrder/makeOrderConfirmationModal";
import { modalsTypes } from "./modalsTypes";

export const modalsComponents: EnumDictionary<string, (props: any) => JSX.Element> = {
    [modalsTypes.makeOrderConfirmationModal]: MakeOrderConfirmationModal,
};