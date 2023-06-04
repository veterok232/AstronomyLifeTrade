/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable react-hooks/rules-of-hooks */
import { isEqual, sortBy } from "lodash";
import React, { useState, useEffect } from "react";
import ReactSelect, { Props } from "react-select";
import { FormGroup, Label, Input } from "reactstrap";
import { localizer } from "../../../localization/localizer";
import { LabeledValue } from "../labeledValue";

type PropsBase = Props<LabeledValue | LabeledValue[], boolean>;

interface SelectProxyProps extends PropsBase {
    allowSelectAll?: boolean;
    selectAllValue?: LabeledValue[];
    selectAllLabelKey?: string;
}

const Select = (props: SelectProxyProps) => {
    if (!props.allowSelectAll) {
        return <ReactSelect {...props} />;
    }

    const isSelectedAll = props.value && isEqual(sortBy(props.value), sortBy(props.selectAllValue));

    const [checked, setChecked] = useState(isSelectedAll);

    useEffect(() => {
        if (!isSelectedAll) {
            setChecked(false);
        }
    }, [props.value]);

    const checkboxOnChange = (value: boolean) => {
        setChecked(value);
        const newValue = value ? props.selectAllValue : null;
        props.onChange(newValue, null);
    };

    const selectOnChange = (value: LabeledValue[]) => {
        const newValue = value?.length === 0 ? null : value;
        props.onChange(newValue, null);
    };

    return (
        <>
            <ReactSelect
                {...props}
                value={props.value ?? []}
                placeholder={checked ? localizer.get("All") : props.placeholder}
                onChange={selectOnChange}
            />
            <FormGroup className="pt-2" check>
                <Label>
                    <Input
                        checked={checked}
                        type="checkbox"
                        onChange={(e: React.ChangeEvent<HTMLInputElement>) => checkboxOnChange(e.target.checked)}
                    />
                    <span className="order-first">{localizer.get(props.selectAllLabelKey)}</span>
                </Label>
            </FormGroup>
        </>
    );
};

export default Select;
