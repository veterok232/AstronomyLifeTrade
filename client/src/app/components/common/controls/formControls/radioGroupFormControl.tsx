import React from "react";
import { FormGroup, Label, Input } from "reactstrap";
import { ValidationList } from "../../presentation/validationList";
import { LabeledValue } from "../labeledValue";
import { FormControlProps, PrefixedField, withFormWrapper } from "./formControlsDecorators";

export interface RadioButtonItem extends LabeledValue {
    content?: JSX.Element;
}

interface RadioGroupProps extends FormControlProps {
    options: Array<RadioButtonItem>;
    inline?: boolean;
    isNumericValue?: boolean;
    valueConverter?: (value: any) => any;
}

const RadioGroupControl = (props: RadioGroupProps) => {
    function valueConverter(value: any) {
        return props.isNumericValue ? +value : value;
    }

    return (
        <PrefixedField {...props} validate={props.validator}>
            {(finalFormProps: any) => {
                const showError =
                    finalFormProps.meta.error && (finalFormProps.meta.dirty || finalFormProps.meta.touched);

                return (
                    <FormGroup className={`radio-group${props.inline ? " inline" : ""}`}>
                        <div className="labels">
                            {props.options.map((ri, ind) => (
                                <Label key={ind}>
                                    <Input
                                        {...finalFormProps.input}
                                        type="radio"
                                        checked={finalFormProps.input.value === ri.value}
                                        value={ri.value}
                                        className={`align-middle ${props.className || ""} ${showError ? "error" : ""}`}
                                        onChange={(val: any) => {
                                            const targetValue = (props.valueConverter ?? valueConverter)(
                                                val.target.value
                                            );
                                            finalFormProps.input.onChange(targetValue);
                                            if (props.onChange) {
                                                props.onChange(targetValue);
                                            }
                                        }}
                                    />
                                    <span className="align-middle">{ri.label}</span>
                                    {ri.content}
                                </Label>
                            ))}
                        </div>
                        <ValidationList showValidations={showError} validations={finalFormProps.meta.error} />
                    </FormGroup>
                );
            }}
        </PrefixedField>
    );
};

export const RadioGroupFormControl = withFormWrapper(RadioGroupControl);
