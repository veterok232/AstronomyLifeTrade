import React from "react";
import { Input } from "reactstrap";
import { AppIcon } from "./appIcon";
import { ControlValidation } from "./controlValidation";

interface Props {
    innerRef: (inputElement: HTMLElement) => void;
    textPosition?: "right" | "center" | "left";
    showError?: boolean;
    showWarning?: boolean;
    showValidationIcons?: boolean;
    errors?: string | string[];
    warnings?: string | string[];
    className?: string;
    readOnly?: boolean;
}

export const InputControl = (props: Props) => {
    const {textPosition, showError, showWarning, showValidationIcons, ...inputProps} = props;
    const validationClassName = `${showWarning ? "warning" : ""} ${showError ? "error" : ""}`;

    return (<>
        <div className={`input-control ${validationClassName}`}>
        <Input {...inputProps}
            className={`${props.className || ""} text-${textPosition || "center"} ${validationClassName}`} />
            {showValidationIcons &&  <AppIcon icon="error" className="validation-icon" />}
        </div>
        <ControlValidation
            showError={showError}
            showWarning={showWarning}
            errors={props.errors}
            warnings={props.warnings} />
    </>);
};