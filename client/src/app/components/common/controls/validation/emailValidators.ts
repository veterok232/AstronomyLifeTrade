import { equals } from "../../../../utils/emailUtils";
import { Constants } from "../../../constants";
import { localizer } from "../../../localization/localizer";
import { composeValidators, textLength } from "./validators";

export const emailFormat = (value: string): string => {
    return value && !(/^[a-zA-Z0-9.!#$%&*+/=?^_`{|}~-]+@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}$/.exec(value))
        ? localizer.get("EmailFormatValidation")
        : undefined;
};

export const emailsUnique = (values: string[]): string[] => {
    const errors: string[] = [];

    if (values.length > 1) {
        values.forEach((value, i) => {
            if (values.some((v, n) => n != i && equals(v, value))) {
                errors[i] = localizer.get("UniqueError");
            }
        });
    }

    return errors.length > 0 ? errors : undefined;
};

export const emailMaxLenghtValidator = (value: string) =>
    textLength(value, Constants.fieldMaxLengths.emailMaxLength);

export function isEmailValid(email: string): string | Promise<string> {
    return composeValidators(emailFormat, emailMaxLenghtValidator)(email);
}