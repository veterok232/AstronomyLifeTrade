import React from "react";
import { Constants } from "../../../../constants";
import { composeValidators, phone } from "../../validation/validators";
import { FormControlProps, withFormWrapper } from "../formControlsDecorators";
import { inputMasks } from "../../inputMasks";
import { MaskedInputFormControl } from "./maskedInputFormControl";

export const PhoneControl = (props: FormControlProps) => {
    return (<MaskedInputFormControl {...props}
        mask={inputMasks.phoneMask}
        placeholder={Constants.phonePlaceholder}
        validator={composeValidators(props.validator, phone)} />);
};

export const PhoneFormControl = withFormWrapper(PhoneControl);