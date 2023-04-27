import React from "react";
import { toast, ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { ToastMessage } from "./toastMessage";

const defaultOptions = {
    autoClose: 5000,
    closeOnClick: true,
    draggable: true,
    pauseOnHover: true,
    position: toast.POSITION.TOP_CENTER,
};

export const NotificationContainer = () => <ToastContainer {...defaultOptions} />;

export const notifications = {
    ...toast,
    error(message: string) {
        toast.error(
            <ToastMessage message={message} />,
            { draggable: false, closeOnClick: false });
    },
    localizedError(messageKey: string) {
        toast.error(<ToastMessage messageKey={messageKey} />);
    },
    invalidFormError() {
        this.localizedError("FormInvalid");
    },
    localizedSuccess(messageKey: string) {
        toast.success(<ToastMessage messageKey={messageKey} />);
    },
    defaultSuccess() {
        this.localizedSuccess("SuccessfullyCompleted");
    },
    defaultRequestError(traceId?: string) {
        toast.error(
            <ToastMessage messageKey="DefaultRequestError" traceId={traceId} />,
            { draggable: false, closeOnClick: false, autoClose: false });
    },
    warning(message: string) {
        toast.warn(<ToastMessage message={message} />);
    },
    localizedWarning(messageKey: string) {
        toast.warn(<ToastMessage messageKey={messageKey} />);
    },
};
