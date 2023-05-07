import { Constants } from "../components/constants";

export const roundFractional = (fractional: number, allowedDecimals = Constants.defaultDecimalPrecision): number => {
    return parseFloat((fractional ?? 0).toFixed(allowedDecimals));
};

export const convertFractionalToPercentage = (fractional?: number): number => {
    return (fractional || fractional === 0) ? fractional * 100 : null;
};

export const convertFractionalToRoundedPercentage = (fractional?: number): number => {
    return roundFractional(convertFractionalToPercentage(fractional));
};

export const convertPercentageToFractional = (percentage?: number): number => {
    return (percentage || percentage === 0) ? percentage / 100 : null;
};

export const convertPercentageToRoundedFractional = (percentage?: number): number => {
    return roundFractional(convertPercentageToFractional(percentage), 4);
};

export const isEmptyNumber = (value?: number): boolean => {
    return !value && value !== 0;
};

export const roundUp = (value: number, digits: number): number => {
    const precision = 10 ** digits;

    return Math.ceil(value * precision) / precision;
};

export const numericConverter = (rawVal: unknown): number => { return rawVal ? +rawVal : undefined; };

export const commaSeparatedStringNumericConverter = (rawVal: string): number =>
    numericConverter(rawVal?.replace(",", "."));