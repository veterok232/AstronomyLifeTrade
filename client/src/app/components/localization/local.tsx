import { FormattedMessage, PrimitiveType } from "react-intl";
import { localizer } from "./localizer";
import React from "react";

export const Local = FormattedMessage;

export function EnumLocal<TEnum, TEnumValue>(
    props: {
        enumObject: TEnum,
        value: TEnumValue,
        values?: Record<string, PrimitiveType>,
    },
) {
    return <>{localizer.getEnumValue(props.enumObject, props.value, props.values)}</>;
}

export function DateLocal(props: { value: Date }) {
    return <>{localizer.formatDate(props.value)}</>;
}

export function DateTimeLocal(props: { value: Date }) {
    return <>{localizer.formatDateTime(props.value)}</>;
}