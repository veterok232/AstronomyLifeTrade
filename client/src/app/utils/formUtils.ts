/* eslint-disable @typescript-eslint/no-explicit-any */
import { FieldMetaState } from "react-final-form";

export const shouldShowValidations = (meta: FieldMetaState<any>, showIfPristine?: boolean):
    {showError: boolean, showWarning: boolean} => {
    const showValidation = showIfPristine || (meta.dirty || meta.touched);
    const showError = showValidation && meta.error;
    const showWarning = showValidation && meta.data.warning;

    return { showError, showWarning };
};