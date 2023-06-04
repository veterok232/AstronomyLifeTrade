import React from "react";
import ReactDatePicker from "react-datepicker";
import { shouldShowValidations } from "../../../../utils/formUtils";
import { localizer } from "../../../localization/localizer";
import { FormControlValidation } from "./formControlValidation";
import { FormControlProps, PrefixedField, withFormWrapper } from "./formControlsDecorators";
import "react-datepicker/dist/react-datepicker.css";

interface DateControlProps extends FormControlProps {
    isTime?: boolean;
    datePopperPlace?: "bottom-start" | "bottom-end";
    strictParsing?: boolean;
}

const DateControl = (props: DateControlProps) => {
    return (
        <PrefixedField name={props.name} validate={props.validator}>
            {(finalFormProps: any) => {
                // eslint-disable-next-line @typescript-eslint/no-unsafe-argument
                const { showError, showWarning } = shouldShowValidations(finalFormProps.meta);

                return (
                    <div
                        className={`${showError ? "form-control--error" : ""} ${
                            showWarning ? "form-control--warning" : ""
                        }`}
                    >
                        <ReactDatePicker
                            placeholderText={props.placeholder || localizer.get("DateFormat")}
                            className={`form-control ${props.className}`}
                            selected={finalFormProps.input.value}
                            showTimeSelect={props.isTime}
                            dropdownMode={"select"}
                            showYearDropdown
                            showMonthDropdown
                            dateFormat={`dd.MM.yyyy${props.isTime ? " hh:mm aa" : ""}`}
                            timeFormat="h:mm"
                            onChange={(val) => {
                                finalFormProps.input.onChange(val);
                                if (props.onChange) {
                                    props.onChange(val);
                                }
                            }}
                            strictParsing={props.strictParsing}
                            onFocus={finalFormProps.input.onFocus}
                            onBlur={finalFormProps.input.onBlur}
                            readOnly={props.readonly}
                            popperPlacement={props.datePopperPlace || "bottom-start"}
                        />
                        <FormControlValidation meta={finalFormProps.meta} />
                    </div>
                );
            }}
        </PrefixedField>
    );
};

export const DateFormControl = withFormWrapper(DateControl);
