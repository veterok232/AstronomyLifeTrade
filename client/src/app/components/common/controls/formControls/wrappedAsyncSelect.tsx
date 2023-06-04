/* eslint-disable react-hooks/exhaustive-deps */
import React, { useState, useEffect } from "react";
import { FormGroup, Label, Input } from "reactstrap";
import { localizer } from "../../../localization/localizer";
import { LabeledValue } from "../labeledValue";
import AsyncSelect from "react-select/async";

interface Props {
    commonProps: any;
    loadOptions: () => void;
    defaultOptions: boolean | LabeledValue[];
    checkboxChecked: boolean;
}

export const WrappedAsyncSelect = (props: Props) => {
    const [checked, setChecked] = useState(props.checkboxChecked);

    useEffect(() => {
        if (!props.checkboxChecked) {
            setChecked(false);
        }
    }, [props.commonProps.value]);

    const checkboxOnChange = (value: boolean) => {
        setChecked(value);
        const newValue = value ? props.commonProps.selectAllValue : null;
        props.commonProps.onChange(newValue, null);
    };

    return (
        <>
            <AsyncSelect
                {...props.commonProps}
                placeholder={checked ? localizer.get("All") : props.commonProps.placeholder}
                loadOptions={props.loadOptions}
                defaultOptions={props.defaultOptions}
            />
            <FormGroup className="pt-2" check>
                <Label>
                    <Input
                        type="checkbox"
                        checked={checked}
                        onChange={(e: React.ChangeEvent<HTMLInputElement>) => checkboxOnChange(e.target.checked)}
                    />
                    {/* eslint-disable-next-line @typescript-eslint/no-unsafe-argument */}
                    <span className="order-first">{localizer.get(props.commonProps.selectAllLabelKey)}</span>
                </Label>
            </FormGroup>
        </>
    );
};
