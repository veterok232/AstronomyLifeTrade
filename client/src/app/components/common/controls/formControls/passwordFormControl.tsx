import React from "react";
import { composeValidators, password } from "../validation/validators";
import { FormControlProps, withFormWrapper } from "./formControlsDecorators";
import { InputFormControl } from "./inputFormControl";

interface Props extends FormControlProps {
    skipPasswordFormatValidation?: boolean;
}

const PasswordControl = (props: Props) => {
    const validator = composeValidators(
        props.validator,
        props.skipPasswordFormatValidation ? undefined : password);

    return <InputFormControl {...props} type="password" validator={validator} />;
};

export const PasswordFormControl = withFormWrapper(PasswordControl);