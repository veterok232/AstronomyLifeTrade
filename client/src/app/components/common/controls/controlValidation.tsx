import React from "react";
import { ValidationList } from "../presentation/validationList";

interface Props {
    showError?: boolean;
    showWarning?: boolean;
    errors?: string | string[];
    warnings?: string | string[];
}

export const ControlValidation = (props: Props) => {
    return (<>
        <ValidationList showValidations={props.showError} validations={props.errors} />
        {props.showError && props.showWarning && <br />}
        <ValidationList showValidations={props.showWarning}
            areWarnings
            validations={props.warnings} />
    </>);
};