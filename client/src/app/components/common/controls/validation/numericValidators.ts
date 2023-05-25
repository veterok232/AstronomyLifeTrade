import { formatValueWithSigns } from "../../../../utils/formattingUtils";
import { localizer } from "../../../localization/localizer";

export interface BetweenConfiguration {
    value: number;
    minValue?: number;
    maxValue?: number;
    valuePrefixSign?: string;
    valuePostfixSign?: string;
    betweenTemplate?: string;
    greaterTemplate?: string;
    lessTemplate?: string;
}

export const integer = (value: string) =>
    value && !/^\+?[1-9]\d*$/.test(value) ? localizer.get("IntegerValidation") : undefined;

export const between = (value: number, minValue?: number, maxValue?: number) =>
    betweenInternal({ value, minValue, maxValue });

const betweenInternal = (configuration: BetweenConfiguration) => {
    const valuePrefixSign = configuration.valuePrefixSign ?? "";
    const valuePostfixSign = configuration.valuePostfixSign ?? "";

    const messageTemplate =
        (configuration.minValue !== undefined &&
            (configuration.maxValue !== undefined
                ? configuration.betweenTemplate ?? "BetweenValidationTemplate"
                : configuration.greaterTemplate ?? "GreaterValidationTemplate")) ||
        (configuration.maxValue !== undefined && (configuration.lessTemplate ?? "LessValidationTemplate"));

    return messageTemplate &&
        ((configuration.maxValue !== undefined && configuration.value > configuration.maxValue) ||
            (configuration.minValue !== undefined && configuration.value < configuration.minValue))
        ? localizer.get(messageTemplate, {
              min: formatValueWithSigns(configuration.minValue, valuePrefixSign, valuePostfixSign),
              max: formatValueWithSigns(configuration.maxValue, valuePrefixSign, valuePostfixSign),
          })
        : undefined;
};

export const moneyBetween = (value: number, minValue?: number, maxValue?: number) =>
    betweenInternal({
        value,
        minValue,
        maxValue,
        valuePrefixSign: "$",
    });

export const percentBetween = (value: number, minValue?: number, maxValue?: number, betweenTemplate?: string) =>
    betweenInternal({
        value,
        minValue,
        maxValue,
        valuePostfixSign: "%",
        betweenTemplate,
    });

const greater = (value: number, minValue: number) =>
    betweenInternal({
        value,
        minValue: isNaN(minValue) ? 0 : minValue,
    });

export const notNegativeNumber = (val: number) => greater(val, 0);

export const moneyGreater = (value: number, minValue: number, greaterTemplate?: string) =>
    betweenInternal({
        value,
        minValue: isNaN(minValue) ? 0 : minValue,
        valuePrefixSign: "$",
        greaterTemplate,
    });

export const notNegativeMoney = (val: number) => moneyGreater(val, 0);

export const less = (value: number, maxValue: number) =>
    betweenInternal({
        value,
        maxValue,
    });

export const moneyLess = (value: number, maxValue: number, lessTemplate?: string) =>
    betweenInternal({
        value,
        maxValue,
        valuePrefixSign: "$",
        lessTemplate,
    });
