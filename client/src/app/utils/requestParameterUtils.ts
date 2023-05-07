/* eslint-disable @typescript-eslint/no-unsafe-argument */
/* eslint-disable @typescript-eslint/no-explicit-any */
import qs from "qs";
import { convertUtcDateToLocal } from "./dateTimeUtils";

const queryPrefix = "?";
const queryParamsDelimiter = "&";
const queryParamKeyValueDelimiter = "=";

const getEncodedValue = (value: string | number | boolean | Date) => {
    return value instanceof Date
        ? convertUtcDateToLocal(value)
        : encodeURIComponent(value);
};

const getQueryPart = (key: string, value: string | number | boolean | Date) => {
    return value === null || value === undefined
        ? null
        : `${encodeURIComponent(key)}=${getEncodedValue(value)}`;
};

const arrayToQueryString = (key: string, values: Array<string | number | boolean | Date>): string => {
    return values.length
        ? values.map(v => getQueryPart(key, v)).join(queryParamsDelimiter)
        : "";
};

export function stringifyObjectToQueryString(obj: any): string {
    return qs.stringify(obj, {
        allowDots: true,
        skipNulls: true,
        serializeDate: convertUtcDateToLocal,
        arrayFormat: "indices",
        encoder: decodeURIComponent,
    });
}

export function parseQueryStringToObject(queryString: string): any {
    return qs.parse(queryString, { allowDots: true, ignoreQueryPrefix: true });
}

/**
 * @deprecated use newest stringifyObjectToQueryString()
 */
export function objToQueryString(obj: any): string {
    const keyValuePairs = [];

    for (const key in obj) {
        const stringifiedValue = (Array.isArray(obj[key]))
            ? arrayToQueryString(key, obj[key])
            : getQueryPart(key, obj[key]);
        keyValuePairs.push(stringifiedValue);
    }

    return keyValuePairs.filter(p => p).join(queryParamsDelimiter);
}

export function getQueryParamValue(key: string, query: string) {
    if (query.startsWith(queryPrefix)) {
        query = query.substring(1);
    }
    const parameters: string[] = query.split(queryParamsDelimiter);
    const parameter = parameters.find(p => p.startsWith(`${key}${queryParamKeyValueDelimiter}`));

    return parameter && decodeURIComponent(parameter.split(queryParamKeyValueDelimiter)[1]);
}