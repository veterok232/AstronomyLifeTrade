import React from "react";
import MaskedInput from "react-text-mask";
import { Input, Button } from "reactstrap";
import { numericConverter } from "../../../../../utils/numberUtils";
import { ValidationList } from "../../../presentation/validationList";
import { InputMask } from "../../inputMasks";
import { composeValidators } from "../../validation/validators";
import { FormControlProps, PrefixedField } from "../formControlsDecorators";
import { between } from "../../validation/numericValidators";

export interface MaskedInputControlProps extends FormControlProps {
    mask: InputMask;
    minValue: number;
    maxValue: number;
    showMaskOnChange?: boolean;
    valueConverter?: (val: any) => any;
    ignoreZero?: boolean;
    applyChangesOnlyOnBlur?: boolean;
}

interface Props {
    finalFormProps: any;
    showError: boolean;
    className?: string;
    maskedProps: any;
    innerRef?: React.Ref<HTMLInputElement>;
    readonly?: boolean;
    minValue: number;
    maxValue: number;
    onChange?: (val: any) => void;
}

const UpDownNumericControl = (props: Props) => {
    const value = +props.finalFormProps.input.value;

    return (
        <div className={`form-group__numeric-wrapper ${props.showError ? "form-group__numeric-wrapper--error" : ""}`}>
            <Input
                readOnly={props.readonly}
                {...props.maskedProps}
                className={`${props.className}`}
                innerRef={props.innerRef}
            />
            <Button
                disabled={props.readonly || value <= props.minValue}
                className="form-control__btn form-control__btn--minus"
                onClick={() => {
                    props.finalFormProps.input.onChange(value - 1);
                    props?.onChange(value - 1);
                }}
            >
                -
            </Button>
            <Button
                disabled={props.readonly || value >= props.maxValue}
                className="form-control__btn form-control__btn--plus"
                onClick={() => {
                    props.finalFormProps.input.onChange(value + 1);
                    props?.onChange(value + 1);
                }}
            >
                +
            </Button>
        </div>
    );
};

export const MaskedUpDownNumericControl = (props: MaskedInputControlProps) => {
    const validator = composeValidators(props.validator, (v: number) => between(v, props.minValue, props.maxValue));

    return (
        <PrefixedField {...props} validate={validator}>
            {(finalFormProps: any) => {
                const showError = finalFormProps.meta.error && finalFormProps.meta.touched;

                return (
                    <>
                        <MaskedInput
                            mask={props.mask.textMask}
                            {...finalFormProps.input}
                            guide={props.showMaskOnChange ?? false}
                            onBlur={
                                props.applyChangesOnlyOnBlur
                                    ? (event: React.FocusEvent<HTMLInputElement>) => {
                                          const val = numericConverter(event.target.value);
                                          finalFormProps.input.onChange(props.ignoreZero ? val || "" : val);
                                      }
                                    : null
                            }
                            onChange={
                                props.applyChangesOnlyOnBlur
                                    ? null
                                    : (event: React.ChangeEvent<HTMLInputElement>) => {
                                          const val = numericConverter(event.target.value);
                                          finalFormProps.input.onChange(props.ignoreZero ? val || "" : val);
                                      }
                            }
                            render={(ref: (inputElement: HTMLElement) => void, maskedProps: any) => (
                                <UpDownNumericControl
                                    maskedProps={maskedProps}
                                    innerRef={ref}
                                    finalFormProps={finalFormProps}
                                    showError={showError}
                                    {...props}
                                />
                            )}
                        />
                        <ValidationList showValidations={showError} validations={finalFormProps.meta.error} />
                    </>
                );
            }}
        </PrefixedField>
    );
};
