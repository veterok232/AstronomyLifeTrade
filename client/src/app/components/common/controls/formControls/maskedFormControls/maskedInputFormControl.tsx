import React from "react";
import MaskedInput from "react-text-mask";
import { shouldShowValidations } from "../../../../../utils/formUtils";
import { InputMask } from "../../inputMasks";
import { FormControlProps, PrefixedField } from "../formControlsDecorators";
import { InputControl } from "../../inputControl";

export interface MaskedInputFormControlProps extends FormControlProps {
    mask: InputMask;
    showMaskOnChange?: boolean;
    valueConverter?: (val: any) => any;
    ignoreZero?: boolean;
    defaultValue?: string | number | readonly string[];
    applyChangesOnlyOnBlur?: boolean;
    onBlur?: (val: any) => void;
    onFocus?: (val: any) => void;
    type?: "password" | "text";
    togglePlaceHolder?: boolean;
    autoComplete?: string;
    textPosition?: "right" | "center" | "left";
    showValidationIcons?: boolean;
}

export const MaskedInputFormControl = (props: MaskedInputFormControlProps) => {
    return <PrefixedField {...props} validate={props.validator}>
        {(finalFormProps: any) => {
            // eslint-disable-next-line @typescript-eslint/no-unsafe-argument
            const { showError, showWarning } = shouldShowValidations(finalFormProps.meta);

            function transformValue(value: string): string {
                let val: any = props.mask.getRawValue(value);
                if (props.valueConverter) {
                    val = props.valueConverter(val);
                }

                return props.ignoreZero && val === 0 ? "" : val;
            }

            function applyChangesOnBlur(): React.FocusEventHandler<HTMLInputElement> {
                return (event: React.FocusEvent<HTMLInputElement>) => {
                    const val = transformValue(event.target.value);
                    finalFormProps.input.onChange(val);

                    if (props.onBlur) {
                        props.onBlur(val);
                    }

                    if (props.togglePlaceHolder) {
                        event.target.placeholder = props.placeholder;
                    }
                };
            }

            function applyChanges(): React.FocusEventHandler<HTMLInputElement> {
                return (event: React.FocusEvent<HTMLInputElement>) => {
                    const val = transformValue(event.target.value);
                    finalFormProps.input.onChange(val);

                    if (props.onChange) {
                        props.onChange(val);
                    }
                };
            }

            return <>
                <MaskedInput mask={props.mask.textMask}
                    {...finalFormProps.input}
                    guide={props.showMaskOnChange ?? false}
                    onBlur={applyChangesOnBlur()}
                    onFocus={props.onFocus}
                    onChange={props.applyChangesOnlyOnBlur
                        ? undefined
                        : applyChanges()}
                    render={(ref: (inputElement: HTMLElement) => void, maskedProps: any) =>
                        <InputControl {...maskedProps}
                            placeholder={props.placeholder}
                            showError={showError}
                            showWarning={showWarning}
                            errors={finalFormProps.meta.error}
                            warnings={finalFormProps.meta.data.warning}
                            innerRef={ref}
                            textPosition={props.textPosition}
                            readOnly={props.readonly} />
                    }
                />
            </>;
        }}
    </PrefixedField>;
};