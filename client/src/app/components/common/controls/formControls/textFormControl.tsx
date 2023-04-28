import React from "react";
import { Validator, composeValidators, defaultTextFieldMaxLength, fixedFieldLength, textLength } from "../validation/validators";
import { FormControlProps, withFormWrapper } from "./formControlsDecorators";
import { InputFormControl } from "./inputFormControl";

interface Props extends FormControlProps {
    maxLength?: number;
    fixedLength?: number;
    defaultValue?: string;
    type?: "password" | "text";
    offDefaultValidation?: boolean;
}

const TextControl = (props: Props) => {
    if (props.maxLength && props.fixedLength ||
        props.maxLength && props.offDefaultValidation ||
        props.fixedLength && props.offDefaultValidation) {
        throw new Error("Should be defined only single length property for text control.");
    }

    let defaultValidator: Validator;
    if (props.offDefaultValidation) {
        defaultValidator = undefined;
    } else if (!props.maxLength && !props.fixedLength) {
        defaultValidator = defaultTextFieldMaxLength;
    } else {
        defaultValidator = props.fixedLength
            ? (val: string) => fixedFieldLength(val, props.fixedLength)
            : (val: string) => textLength(val, props.maxLength);
    }

    return <InputFormControl {...props} type={props.type ?? "text"}
        validator={composeValidators(defaultValidator, props.validator)}
        value={props.defaultValue} />;
};

export const TextFormControl = withFormWrapper(TextControl);