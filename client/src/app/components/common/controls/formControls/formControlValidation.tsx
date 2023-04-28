import { FieldMetaState } from "react-final-form";
import { shouldShowValidations } from "../../../../utils/formUtils";
import { ControlValidation } from "../controlValidation";
import React from "react";

interface Props {
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    meta: FieldMetaState<any>
    showIfPristine?: boolean;
}
export const FormControlValidation = (props: Props) => {
    const { showError, showWarning } = shouldShowValidations(props.meta, props.showIfPristine);

    return (<ControlValidation
        showError={showError}
        showWarning={showWarning}
        errors={props.meta.error}
        warnings={props.meta.data.warning} />);
};