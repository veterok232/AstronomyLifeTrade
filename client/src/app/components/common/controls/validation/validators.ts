import { PrimitiveType } from "react-intl";
import { localizer } from "../../../localization/localizer";
import { datesStore } from "../../../../infrastructure/stores/datesStore";
import { addDays } from "date-fns";
import { getDateYearsAgo } from "../../../../utils/dateTimeUtils";
import { Constants } from "../../../constants";
import { getFileExtension } from "../../../../utils/fileUtils";
import { imageSizeValidator } from "./fileValidators";

export type Validator = (value: any) => string | Promise<string>;
type ValidatorToString = (value: any) => string;
type ValidatorToPromise = (value: any) => Promise<string>;

export function composeValidators(...validators: ValidatorToString[]): ValidatorToString;
export function composeValidators(...validators: ValidatorToPromise[]): ValidatorToPromise;
export function composeValidators(...validators: Validator[]): Validator;
export function composeValidators(...validators: Validator[]) {
    return (value: any): string | Promise<string> => {
        for (const validator of validators.filter(v => v)) {
            const error = validator(value);
            if (error) {
                if (typeof error === "string") {
                    return error;
                } else {
                    return error;
                }
            }
        }
    };
}

export const required = (value: any) => (!value || value.length === 0) && value !== false && value !== 0 ? localizer.get("FieldRequired") : undefined;

export const requiredIf = (value: any, condition: boolean) => condition ? required(value) : undefined;

export const requiredNonWhitespace = (value: string) => required(value?.trim());

export const noAnyWhitespace = (value: string) => {
    return (/\s/g.test(value)) ? localizer.get("NonContainAnyWhitespace") : undefined;
};

export const requiredNonWhitespaceIf = (value: string, condition: boolean) => {
    return condition ? requiredNonWhitespace(value) : undefined;
};

export const customRequired = (value: any, messageKey: string) =>
    (!value || value.length === 0) && value !== false && value !== 0 ? localizer.get(messageKey) : undefined;

export const customRequiredIf = (value: any, condition: boolean, messageKey: string) =>
    condition ? customRequired(value, messageKey) : undefined;

export const isChecked = (value: any) => !value ? localizer.get("FieldRequired") : undefined;

export const nonWhitespace = (value: string) => {
    value = value?.trim();

    return value === "" ? ((!value) ? localizer.get("NonContainWhitespaces") : undefined) : undefined;
};

export const dateRange = (
    value: Date,
    messageKey: string,
    minValue?: Date,
    maxValue?: Date,
    messageValues?: Record<string, PrimitiveType>,
) => {
    return ((maxValue !== undefined && value > maxValue) || (minValue !== undefined && value < minValue))
        ? localizer.get(messageKey, messageValues)
        : undefined;
};

export const minDate = (value: Date, minValue: Date | string, messageKey: string) =>
    dateRange(value, messageKey, new Date(minValue), undefined);

export const maxDate = (value: Date, maxValue: Date, messageKey: string) => dateRange(value, messageKey, undefined, maxValue);

export const greaterThanToday = async (value: Date): Promise<string> => {
    const minDateValue = addDays(await datesStore.getCurrentCstDate(), 1);

    return minDate(value, minDateValue, "MinDateIsFutureValidation");
};

export const greaterOrEqualThanToday = async (value: Date): Promise<string> => {
    return minDate(value, await datesStore.getCurrentCstDate(), "MinDateIsTodayValidation");
};

export const lessOrEqualThanToday = async (value: Date): Promise<string> => {
    return maxDate(value, await datesStore.getCurrentCstDate(), "MaxDateIsTodayValidation");
};

export const legalAge = (value: Date, ageValidationMessageKey: string) => {
    return dateRange(
        value,
        ageValidationMessageKey,
        getDateYearsAgo(Constants.maxAge),
        getDateYearsAgo(Constants.minAge),
        { minAge: Constants.minAge, maxAge: Constants.maxAge },
    );
};

export const customValidator = (showIf: any, messageKey: string) => showIf ? localizer.get(messageKey) : undefined;

export function textLength(
    value: string | undefined | null,
    max: number,
    min = 0,
    validationMessageTemplateKey = "TextFieldMaxLengthValidationTemplate",
) {
    return value && (value.length < min || value.length > max) ? localizer.get(validationMessageTemplateKey, { min, max }) : undefined;
}

export function textLengthBetween(
    value: string | undefined | null,
    min: number,
    max: number,
    validationMessageTemplateKey = "TextFieldLengthBetweenValidationTemplate",
) {
    return textLength(value, max, min, validationMessageTemplateKey);
}

export const shortTextFieldLength = (value?: string) =>
    textLength(value, Constants.fieldMaxLengths.shortText);

export const defaultTextFieldMaxLength = (value?: string) =>
    textLength(value, Constants.fieldMaxLengths.defaultText);

export const fixedFieldLength = (
    value: string,
    length: number,
    validationMessageTemplateKey = "TextFieldFixedLengthValidationTemplate",
) => textLength(value, length, length, validationMessageTemplateKey);

export const password = (value: string) => {
    if (!value) {
        return required(value);
    }
    if (value.length < Constants.password.minLength || value.length > Constants.password.maxLength) {
        return localizer.get("SetupPasswordErrors_InvalidLength");
    }
    if (/\s/.test(value)) {
        return localizer.get("SetupPasswordErrors_IncludeSpace");
    }
    if (!Constants.password.specialCharactersExpression.test(value)) {
        return localizer.get("SetupPasswordErrors_MissingRequiredCharacters");
    }
    return undefined;
};

export const phone = (value?: string) => {
    return customValidator(value &&
        (value.length !== Constants.specificFieldLengths.phone ||
            isNaN(+value)), "InvalidPhoneNumber");
};

const fileExtensionValidator = (file?: File, extensionsList?: string[]) => {
    if (file && !extensionsList.some(extension => getFileExtension(file.name) === extension)) {
        return localizer.get("FileExtensionValidation");
    }
};

export const imageFileExtensionValidator = (file?: File) => {
    return fileExtensionValidator(file, Constants.availableImageFileExtensions);
};

export const imageValidator = composeValidators(imageFileExtensionValidator, imageSizeValidator);