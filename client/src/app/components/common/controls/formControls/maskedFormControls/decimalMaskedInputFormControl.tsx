/* eslint-disable react-hooks/rules-of-hooks */
import { isNumber, isString } from "lodash";
import React, { useState, useEffect } from "react";
import MaskedInput from "react-text-mask";
import { shouldShowValidations } from "../../../../../utils/formUtils";
import { Constants } from "../../../../constants";
import { localizer } from "../../../../localization/localizer";
import { InputControl } from "../../inputControl";
import { InputMask, getLimitedDecimalMask } from "../../inputMasks";
import { FormControlProps, PrefixedField } from "../formControlsDecorators";

interface DecimalMaskedInputFormControlProps extends FormControlProps {
    mask?: InputMask;
    showMaskOnChange?: boolean;
    ignoreZero?: boolean;
    textPosition?: "right" | "center" | "left";
    applyChangesOnlyOnBlur?: boolean;
    showValidationIcons?: boolean;
}

export const DecimalMaskedInputFormControl = (props: DecimalMaskedInputFormControlProps) => {
    const mask = props.mask || getLimitedDecimalMask();

    const convertToFloat = (val: string | number): number => {
        if (isNumber(val)) {
            return val;
        }

        const result = parseFloat(mask.getRawValue(val));
        return isNaN(result) ? undefined : result;
    };

    const valueConverter = (val: string | number): string => {
        const result = isString(val) ? convertToFloat(val) : val;
        return result === undefined || (result === 0 && props.ignoreZero)
            ? undefined
            : localizer.formatNumber(result, Constants.defaultDecimalPrecision);
    };

    return (
        <PrefixedField {...props} validate={props.validator}>
            {(finalFormProps: any) => {
                const [value, setValue] = useState<string>(finalFormProps.input.value);

                useEffect(() => setValue(valueConverter(finalFormProps.input.value)), [finalFormProps.input.value]);

                const { showError, showWarning } = shouldShowValidations(finalFormProps.meta);

                if (!finalFormProps.meta.visited) {
                    finalFormProps.input.onChange(convertToFloat(finalFormProps.input.value));
                }

                return (
                    <>
                        <MaskedInput
                            mask={mask.textMask}
                            {...finalFormProps.input}
                            value={value ?? null}
                            defaultValue=""
                            guide={props.showMaskOnChange ?? false}
                            onBlur={(event: React.FocusEvent<HTMLInputElement>) => {
                                setValue(valueConverter(event.target.value));
                                finalFormProps.input.onChange(convertToFloat(event.target.value));
                            }}
                            onChange={
                                props.applyChangesOnlyOnBlur
                                    ? null
                                    : (event: React.ChangeEvent<HTMLInputElement>) => {
                                          setValue(event.target.value);
                                          finalFormProps.input.onChange(convertToFloat(event.target.value));
                                      }
                            }
                            render={(ref: (inputElement: HTMLElement) => void, maskedProps: any) => (
                                <InputControl
                                    {...maskedProps}
                                    textPosition={props.textPosition}
                                    placeholder={props.placeholder}
                                    showError={showError}
                                    showWarning={showWarning}
                                    errors={finalFormProps.meta.error}
                                    warnings={finalFormProps.meta.data.warning}
                                    innerRef={ref}
                                    showValidationIcons={props.showValidationIcons}
                                />
                            )}
                        />
                    </>
                );
            }}
        </PrefixedField>
    );
};