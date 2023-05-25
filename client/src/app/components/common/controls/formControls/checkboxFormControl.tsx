import React from "react";
import { FormControlProps, PrefixedField } from "./formControlsDecorators";
import { CheckboxControl } from "../checkboxControl";

export const CheckboxFormControl = (props: FormControlProps) => {
    const { className } = props;

    return (
        <PrefixedField {...props} validate={props.validator}>
            {(finalFormProps: any) => {
                const showError =
                    finalFormProps.meta.error && (finalFormProps.meta.dirty || finalFormProps.meta.touched);

                return (
                    <CheckboxControl
                        className={`${className + " form-control-wrapper" || "form-control-wrapper"} ${
                            showError ? "checkbox-validation-error" : ""
                        }`}
                        label={props.label}
                        checked={finalFormProps.input.value}
                        error={showError ? finalFormProps.meta.error : undefined}
                        readonly={props.readonly}
                        onChange={(val: any) => {
                            finalFormProps.input.onChange(val);
                            if (props.onChange) {
                                props.onChange(val);
                            }
                        }}
                    />
                );
            }}
        </PrefixedField>
    );
};
