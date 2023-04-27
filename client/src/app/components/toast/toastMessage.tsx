import React from "react";
import { localizer } from "../localization/localizer";

interface Props {
    message?: string;
    messageKey?: string;
    traceId?: string
}

export function ToastMessage(props: Props) {
    const message = props.message ?? localizer.get(props.messageKey);
    const tracePart = props.traceId ? `. ${localizer.get("Error")}: ${props.traceId}` : "";

    return (<div className="toast-message">{message}{tracePart}</div>);
}