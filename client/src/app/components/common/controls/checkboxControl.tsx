import React, { useState, useEffect, ChangeEvent } from "react";
import { FormGroup, Label, Input } from "reactstrap";
import { localizer } from "../../localization/localizer";
import { ValidationList } from "../presentation/validationList";
import { checkboxInputType } from "./formControls/inputFormControl";

interface Props {
    checked: boolean;
    label?: string | JSX.Element;
    className?: string;
    error?: string;
    readonly?: boolean;
    onChange?: (val: any) => void;
}

export const CheckboxControl = (props: Props) => {
    const { label, error, onChange } = props;
    const [checked, setChecked] = useState(props.checked);

    useEffect(() => setChecked(props.checked), [props.checked]);

    const onClick = (e: React.MouseEvent<HTMLLabelElement, MouseEvent>) => {
        e.stopPropagation();
    };

    return (
        <FormGroup className={props.className} check>
            <Label onClick={onClick}>
                <Input
                    checked={checked}
                    type={checkboxInputType}
                    disabled={props.readonly}
                    onChange={(val: ChangeEvent<HTMLInputElement>) => {
                        setChecked(val.target.checked);
                        if (onChange) {
                            onChange(val.target.checked);
                        }
                    }}
                />
                {label && (typeof label === "string" ? <span>{localizer.get(label)}</span> : label)}
            </Label>
            <ValidationList showValidations={!!error} validations={error} />
        </FormGroup>
    );
};
