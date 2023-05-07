import React from "react";
import { localizeNotAvailableShort } from "./localizationUtils";
import { isEmptyNumber } from "./numberUtils";
import { localizer } from "../components/localization/localizer";
import { Constants } from "../components/constants";

const metricPrefixes = ["B", "KB", "MB", "GB", "TB"];

export function getConditionalText(condition: boolean, textIfSucceeded: string, textIfFailed?: string) {
    return condition
        ? textIfSucceeded
        : textIfFailed || "";
}

export function getFullName(firstName: string, lastName: string, middleName?: string): string | undefined {
    if (!firstName || !lastName) {
        return undefined;
    }
    return !middleName ? `${firstName} ${lastName}` : `${firstName} ${middleName} ${lastName}`;
}

export function getSlashSeparatedValues(left: string | JSX.Element, right: string | JSX.Element): JSX.Element {
    return right ? (<>{left} / {right}</>) : (<>{left}</>);
}

export function getNullableValueByDash(value: string | null | undefined): string {
    return (value === null || value === undefined) ? "-" : value;
}

export const getNullableStringRepresentation = (value?: string): JSX.Element =>
    <>{value || localizeNotAvailableShort()}</>;

export const getNullableNumberRepresentation = (value?: number): JSX.Element =>
    <>{isEmptyNumber(value) ? localizeNotAvailableShort() : value}</>;

export const joinWithSeparator = (value: (string | number)[], separator = ", "): string => {
    return (Array.isArray(value) && value.length) ? value.join(separator) : null;
};

export const formatFileSize = (sizeInBytes: number): string => {
    let fullThousands = 0;

    while (sizeInBytes > 1000) {
        sizeInBytes /= 1000;
        fullThousands++;
    }

    return `${sizeInBytes.toFixed(fullThousands ? 1 : 0)} ${metricPrefixes[fullThousands]}`;
};

export const formatValueWithSigns = (value: number, valuePrefixSign: string, valuePostfixSign: string) =>
    `${valuePrefixSign}${value}${valuePostfixSign}`;

export const getPagingInfo = (pageNumber: number, pageSize: number, rowsLength: number, totalCount: number) =>
    localizer.get("ShowPagingInfoTemplate", {
        from: totalCount === 0
            ? 0
            : (pageNumber - 1) * pageSize + 1,
        to: (pageNumber - 1) * pageSize + rowsLength,
        totalCount: totalCount
    });

export const getLimitedSizeString = (val: string, size = Constants.limitedStringMaxLength) => {
    return val?.length > size ? `${val.substr(0, size)}...` : val;
};

export const generateStubbedDisplayValue = (
    showCalculationResult: boolean,
    result: string,
    placeholder = "-") => (showCalculationResult ? result : placeholder);

export const formatMinMaxMonthRange = (min: number, max: number, separator = " - "): string => {
    if (isEmptyNumber(min) && isEmptyNumber(max)) {
        return null;
    }

    let values: string;

    if (min === max) {
        values = `${min}`;
    } else if (isEmptyNumber(min) && !isEmptyNumber(max)) {
        values = `${0}${separator}${max}`;
    } else values = `${min}${separator}${max}`;

    return `${values} ${localizer.get("MonthsTimeUnit")}`;
};

export const getFormattedPhoneNumber = (phone: string) => {
    return phone ? phone?.replace(/(\d{3})(\d{3})(\d{4})/g, "($1) $2-$3") : null;
};