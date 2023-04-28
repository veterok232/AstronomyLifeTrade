import React from "react";
import Input, { InputType } from "reactstrap/es/Input";
import { FormControlProps, PrefixedField } from "./formControlsDecorators";
import { shouldShowValidations } from "../../../../utils/formUtils";
import { FormControlValidation } from "./formControlValidation";

interface FormInputProps {
    type: InputType;
    disabled?: boolean;
    converter?: (rawVal: any) => any;
    onChange?: (val: any) => void;
    maxLength?: number;
}

export const checkboxInputType = "checkbox";

const getInputValue = (rawValue: any, inputType: InputType) =>
    inputType === checkboxInputType
        ? rawValue.target.checked
        : rawValue.target.value;

export const InputFormControl = (props: FormControlProps & FormInputProps & { value?: any }) => {
    return <PrefixedField {...props} validate={props.validator}>
        {(finalFormProps: any) => {
            // eslint-disable-next-line @typescript-eslint/no-unsafe-argument
            const { showError, showWarning } = shouldShowValidations(finalFormProps.meta);

            return <>
                <Input {...finalFormProps.input}
                    maxLength={props.maxLength}
                    placeholder={props.placeholder}
                    type={props.type}
                    readOnly={props.readonly}
                    disabled={props.disabled}
                    className={`${props.className || ""} ${showError ? "error" : ""} ${showWarning ? "warning" : ""}`}
                    onChange={(val: any) => {
                        const srcValue: any = getInputValue(val, props.type);
                        const targetValue = props.converter ? props.converter(srcValue) : srcValue;
                        finalFormProps.input.onChange(targetValue);
                        if (props.onChange) {
                            props.onChange(targetValue);
                        }
                    }} />
                <FormControlValidation meta={finalFormProps.meta} />
            </>;
        }}
    </PrefixedField>;
};