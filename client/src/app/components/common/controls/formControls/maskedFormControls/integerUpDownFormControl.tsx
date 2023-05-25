import React from "react";
import { getLimitedIntegerMask } from "../../inputMasks";
import { FormControlProps, withFormWrapper } from "../formControlsDecorators";
import { MaskedUpDownNumericControl } from "./maskedUpDownNumericControl";

interface Props extends FormControlProps {
    minValue: number;
    maxValue: number;
    allowedDigits?: number;
    ignoreZero?: boolean;
    applyChangesOnlyOnBlur?: boolean;
}

const IntegerUpDownControl = (props: Props) => {
    return (
        <MaskedUpDownNumericControl
            {...props}
            className={`${props.className} text-center`}
            mask={getLimitedIntegerMask(props.allowedDigits)}
            valueConverter={(val: string) => {
                const result = parseInt(val);
                return isNaN(result) ? null : result;
            }}
        />
    );
};

export const IntegerUpDownFormControl = withFormWrapper(IntegerUpDownControl);