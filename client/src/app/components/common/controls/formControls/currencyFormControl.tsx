import React from "react";
import { getCurrencyMask } from "../inputMasks";
import { FormControlProps, withFormWrapper } from "./formControlsDecorators";
import { DecimalMaskedInputFormControl } from "./maskedFormControls/decimalMaskedInputFormControl";

interface Props extends FormControlProps {
    integerDigitsLimit?: number;
    textPosition?: "right" | "center" | "left";
    applyChangesOnlyOnBlur?: boolean;
    hideCurrencyPrefix?: boolean;
    allowNegative?: boolean;
    showValidationIcon?: boolean;
}

const CurrencyControl = (props: Props) => (
    <DecimalMaskedInputFormControl
        {...props}
        applyChangesOnlyOnBlur
        mask={getCurrencyMask(props.integerDigitsLimit, props.hideCurrencyPrefix, props.allowNegative)}
        showValidationIcons={props.showValidationIcon}
    />
);

export const CurrencyFormControl = withFormWrapper(CurrencyControl);