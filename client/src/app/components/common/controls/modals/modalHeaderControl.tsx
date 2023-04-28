import React from "react";
import { ModalHeader, ModalHeaderProps } from "reactstrap";

export const ModalHeaderControl = (props: ModalHeaderProps) => {
    return <ModalHeader className={`pl-5 pb-0 d-flex flex-column align-items-center border-bottom-0 ${props.className || ""}`}
                toggle={props.toggle}>
        {props.children}
    </ModalHeader>;
};