import { uniqBy, isObject, isEqual } from "lodash";
import React, { useState, useEffect } from "react";
import { shouldShowValidations } from "../../../../utils/formUtils";
import { localizer } from "../../../localization/localizer";
import { LabeledValue } from "../labeledValue";
import { FormControlValidation } from "./formControlValidation";
import { FormControlProps, PrefixedField, withFormWrapper } from "./formControlsDecorators";
import AsyncSelect from "react-select/async";
import { WrappedAsyncSelect } from "./wrappedAsyncSelect";
import Select from "./select";

interface RichSelectControlProps extends FormControlProps {
    options?: LabeledValue[];
    multi?: boolean;
    optionsLoader?: (inputValue?: any) => Promise<Array<LabeledValue>>;
    searchable?: boolean;
    clearable?: boolean;
    defaultValue?: LabeledValue;
    showErrorIfPristine?: boolean;
    allowSelectAll?: boolean;
    selectAllLabelKey?: string;
    selectAllValue?: Array<LabeledValue>;
    checkboxChecked?: boolean;
}

const isOptionDisabled = (option: LabeledValue | LabeledValue[]) => (option as LabeledValue)?.disabled;

const RichSelectControl = (props: RichSelectControlProps) => {
    // When component becomes readonly, readonly flag shouldn't be applied directly to select component,
    // firstly this component should be refreshed in order to remove focus
    const [isReadonly, setIsReadonly] = useState(props.readonly);

    useEffect(() => {
        setIsReadonly(props.readonly);
    }, [props.readonly]);

    if ((props.options && props.optionsLoader) || (!props.options && !props.optionsLoader)) {
        throw new Error("One (and only one) of \"options\" or \"optionsLoader\" should be defined in props");
    }

    const [defaultAsyncOptions, setDefaultAsyncOptions] = useState<Array<LabeledValue>>();
    const [asyncOptions, setAsyncOptions] = useState<Array<LabeledValue>>();

    const loadOptions = async (inputValue?: string) => {
        const options = await props.optionsLoader(inputValue);
        setAsyncOptions(options);
        if (!defaultAsyncOptions) {
            setDefaultAsyncOptions(options);
        }

        return options;
    };

    return (
        <PrefixedField name={props.name} validate={props.validator}>
            {(finalFormProps: any) => {
                const { showError, showWarning } = shouldShowValidations(
                    finalFormProps.meta,
                    props.showErrorIfPristine
                );
                const options =
                    props.options || uniqBy(asyncOptions?.concat(defaultAsyncOptions ?? []) || [], (o) => o.value);

                const getSelectedOptions = (
                    value: string | string[] | object,
                    options: LabeledValue[]
                ): LabeledValue | LabeledValue[] | null => {
                    if (props.allowSelectAll && !finalFormProps.input.value) {
                        return null;
                    }

                    return options.filter((o) =>
                        Array.isArray(value)
                            ? value.includes(o.value)
                            : value === o.value || (isObject(value) && isEqual(value, o.value))
                    );
                };

                const commonProps = {
                    value: getSelectedOptions(finalFormProps.input.value, options),
                    className: `form-group-select ${showError ? "error" : ""} ${showWarning ? "warning" : ""}`,
                    classNamePrefix: "form-group-select",
                    onChange: (val: any) => {
                        finalFormProps.input.onChange(
                            val && ((Array.isArray(val) && val.map((v: LabeledValue) => v.value)) || val.value)
                        );
                        if (props.onChange) {
                            props.onChange(
                                val && (val.value === undefined ? val.map((v: LabeledValue) => v.value) : val.value)
                            );
                        }
                    },
                    onInputChange: (inputValue: string) => {
                        if (inputValue && !props.multi) {
                            finalFormProps.input.onChange(undefined);
                            props.onChange?.(undefined);
                        }
                    },
                    isDisabled: isReadonly,
                    isMulti: props.multi,
                    isSearchable: props.searchable ?? false,
                    isClearable: props.clearable,
                    placeholder: props.placeholder ?? localizer.get("Choose"),
                    allowSelectAll: props.allowSelectAll,
                    selectAllLabelKey: props.selectAllLabelKey,
                    selectAllValue: props.selectAllValue,
                };

                const defaultValue =
                    options &&
                    (props.defaultValue ? props.defaultValue : options.length === 1 ? options[0] : undefined);

                return (
                    <>
                        {props.options ? (
                            <Select
                                {...commonProps}
                                options={props.options}
                                defaultValue={defaultValue}
                                isOptionDisabled={isOptionDisabled}
                            />
                        ) : props.selectAllValue ? (
                            <WrappedAsyncSelect
                                commonProps={commonProps}
                                loadOptions={loadOptions}
                                defaultOptions={defaultAsyncOptions || true}
                                checkboxChecked={props.checkboxChecked}
                            />
                        ) : (
                            <AsyncSelect
                                {...commonProps}
                                loadOptions={loadOptions}
                                defaultValue={defaultValue}
                                defaultOptions={defaultAsyncOptions || true}
                            />
                        )}
                        <FormControlValidation meta={finalFormProps.meta} showIfPristine={props.showErrorIfPristine} />
                    </>
                );
            }}
        </PrefixedField>
    );
};

export const RichSelectFormControl = withFormWrapper(RichSelectControl);
