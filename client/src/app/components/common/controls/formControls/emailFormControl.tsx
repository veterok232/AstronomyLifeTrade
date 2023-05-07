import React from "react";
import { emailFormat } from "../validation/emailValidators";
import { composeValidators, textLength } from "../validation/validators";
import { FormControlProps, withFormWrapper } from "./formControlsDecorators";
import { InputFormControl } from "./inputFormControl";

interface Props extends FormControlProps {
    skipEmailFormatValidation?: boolean;
}

const emailLength = 50;

const EmailControl = (props: Props) => {
    const maxLengthValidator = (val: string) => textLength(val, emailLength);

    const validator = composeValidators(
        maxLengthValidator,
        props.validator,
        props.skipEmailFormatValidation ? undefined : emailFormat);

    return <InputFormControl {...props} type="email" validator={validator} />;
};

export const EmailFormControl = withFormWrapper(EmailControl);