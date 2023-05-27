import React from "react";
import { ModalBody} from "reactstrap";
import { modalsStore } from "../../../infrastructure/stores/modalsStore";
import { ModalHeaderControl } from "../../common/controls/modals/modalHeaderControl";
import { modalsTypes } from "../../layout/modals/modalsTypes";
import { Local } from "../../localization/local";
import { sharedHistory } from "../../../infrastructure/sharedHistory";
import { getRoute } from "../../../utils/routeUtils";
import { routeLinks } from "../../layout/routes/routeLinks";

interface Props {
    orderNumber: number;
}

const closeModal = () => {
    modalsStore.closeModal(modalsTypes.makeOrderConfirmationModal);
    sharedHistory.push(getRoute(routeLinks.catalog.root));
};

export const MakeOrderConfirmationModal = (props: Props) => {
    return (
        <>
            <ModalHeaderControl toggle={closeModal}>
                <p className="title text-center">
                    <Local id="MakeOrderConfirmationModal.Title" />
                </p>
            </ModalHeaderControl>
            <hr className="w-50" />
            <ModalBody className="d-flex flex-column align-items-center px-sm-5 px-3 pt-2">
                <p>
                    Спасибо за Ваш заказ! Наш менеджер свяжется с Вами для уточнения деталей по указанному номеру телефона.
                </p>
                <p>
                    Номер вашего заказа: {props.orderNumber}.
                </p>
            </ModalBody>
        </>
    );
};