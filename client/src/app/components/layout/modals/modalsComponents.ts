import { EnumDictionary } from "../../../infrastructure/enums/enumDictionary";
import { ConfirmationModal } from "../../common/confirmationModal";
import { MakeOrderConfirmationModal } from "../../orders/makeOrder/makeOrderConfirmationModal";
import { OrderDetailsModal } from "../../orders/orders/modals/orderDetailsModal";
import { modalsTypes } from "./modalsTypes";

export const modalsComponents: EnumDictionary<string, (props: any) => JSX.Element> = {
    [modalsTypes.makeOrderConfirmationModal]: MakeOrderConfirmationModal,
    [modalsTypes.orderDetailsModal]: OrderDetailsModal,
    [modalsTypes.confirmationModal]: ConfirmationModal,
};