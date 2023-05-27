import React from "react";
import { textLength, defaultTextFieldMaxLength, composeValidators } from "../validation/validators";
import { FormControlProps, withFormWrapper } from "./formControlsDecorators";
import { InputFormControl } from "./inputFormControl";

interface Props extends FormControlProps {
    maxLength?: number;
    ignoreDefaultValidator?: boolean;
}

const TextAreaControl = (props: Props) => {
    const defaultValidator = props.maxLength
        ? (val: string) => textLength(val, props.maxLength)
        : defaultTextFieldMaxLength;

    return (
        <InputFormControl
            {...props}
            type="textarea"
            validator={
                props.ignoreDefaultValidator ? props.validator : composeValidators(defaultValidator, props.validator)
            }
        />
    );
};

export const TextAreaFormControl = withFormWrapper(TextAreaControl);
