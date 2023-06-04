import React from "react";
import { ModalBody, ModalFooter, Button } from "reactstrap";
import { modalsStore } from "../../infrastructure/stores/modalsStore";
import { modalsTypes } from "../layout/modals/modalsTypes";
import { localizer } from "../localization/localizer";
import { ModalHeaderControl } from "./controls/modals/modalHeaderControl";

export type ActionHandler = () => void | Promise<void>;

export interface ConfirmationOptions {
    onConfirmClick: ActionHandler;
    onCancelClick?: ActionHandler;
    header?: string | JSX.Element;
    body?: string | JSX.Element;
    confirmCaption?: string;
    confirmButtonAppearance?: "primary" | "secondary" | "danger";
    cancelCaption?: string;
    hideCancelButton?: boolean;
}

export const showConfirmation = (options: ConfirmationOptions) => {
    modalsStore.openModal({
        modalType: modalsTypes.confirmationModal,
        modalProps: options,
    });
};

export const ConfirmationModal = (props: ConfirmationOptions) => {
    const close = () => modalsStore.closeModal(modalsTypes.confirmationModal);

    const onCancel = async () => {
        await props.onCancelClick?.();
        close();
    };

    const onConfirm = async () => {
        await props.onConfirmClick();
        close();
    };

    return (
        <>
            <ModalHeaderControl toggle={onCancel}>
                <b className="d-block mt-2">{props.header || localizer.get("DefaultConfirmation_Header")}</b>
            </ModalHeaderControl>
            <hr className="mt-3"/>
            <ModalBody className="text-center px-3 px-md-5 py-4">
                {props.body || localizer.get("DefaultConfirmation_Body")}
            </ModalBody>
            <ModalFooter className="flex-nowrap border-0 px-3 px-md-5 pt-0 pb-3 pb-md-5 justify-content-center">
                {!props.hideCancelButton && (
                    <Button onClick={onCancel} className="button-cancel w-50">
                        {props.cancelCaption || localizer.get("DefaultConfirmation_CancelCaption")}
                    </Button>
                )}
                <Button onClick={onConfirm} className={`button-${props.confirmButtonAppearance || "approve"} w-50`}>
                    {props.confirmCaption || localizer.get("DefaultConfirmation_ConfirmCaption")}
                </Button>
            </ModalFooter>
        </>
    );
};
