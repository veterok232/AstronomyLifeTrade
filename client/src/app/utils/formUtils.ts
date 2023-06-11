/* eslint-disable no-continue */
/* eslint-disable @typescript-eslint/no-explicit-any */
import { FieldMetaState } from "react-final-form";
import { convertUtcDateToLocal } from "./dateTimeUtils";
import { isNumber } from "lodash";

export const shouldShowValidations = (meta: FieldMetaState<any>, showIfPristine?: boolean):
    {showError: boolean, showWarning: boolean} => {
    const showValidation = showIfPristine || (meta.dirty || meta.touched);
    const showError = showValidation && meta.error;
    const showWarning = showValidation && meta.data.warning;

    return { showError, showWarning };
};

interface KeyValuePair {
    key: string;
    value: string;
}

const getSingleItem = (key: string, value: any): KeyValuePair => {
    if (value instanceof Date) {
        return { key, value: convertUtcDateToLocal(value) };
    }

    return { key, value };
};

const getArrayItem = (key: string, values: any[]): KeyValuePair[] => {
    return values.map((x) => getSingleItem(key, x));
};

const appendItem = (formData: FormData, item: KeyValuePair) => {
    if (item.value || (isNumber(item.value) && item.value == 0)) {
        formData.append(item.key, item.value);
    }
};

export function objToFormData(obj: any): FormData {
    const formData = new FormData();

    for (const key in obj) {
        if (obj[key] instanceof FileList) {
            const fileList = obj[key];

            for (let i = 0; i < fileList.length; i++) {
                formData.append(key, fileList[i]);
            }

            continue;
        }

        if (Array.isArray(obj[key])) {
            // eslint-disable-next-line @typescript-eslint/no-unsafe-argument
            getArrayItem(key, obj[key]).forEach((item: KeyValuePair) => appendItem(formData, item));
        } else {
            appendItem(formData, getSingleItem(key, obj[key]));
        }
    }

    return formData;
}
