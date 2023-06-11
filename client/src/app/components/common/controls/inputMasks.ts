import { Mask } from "react-text-mask";
import createNumberMaskCore  from "text-mask-addons/dist/createNumberMask";
import { Constants } from "../../constants";

function createNumberMask(mask: TextMask): InputMask {
    return {
        textMask: createNumberMaskCore(mask),
        getRawValue: value => value?.replace(mask.prefix, "").replace(mask.suffix, "")
    };
}

export type InputMask = {
    getRawValue: (value: string) => string;
    textMask: Mask | ((value: string) => Mask);
}

type TextMask = {
    prefix?: string;
    suffix?: string;
    includeThousandsSeparator?: boolean;
    thousandsSeparatorSymbol?: string;
    allowDecimal?: boolean;
    decimalSymbol?: string;
    decimalLimit?: number;
    integerLimit?: number;
    requireDecimal?: boolean;
    allowNegative?: boolean;
    allowLeadingZeroes?: boolean;
}

const unprefixedRawNumberConfig: TextMask = { prefix: "", includeThousandsSeparator: false };

function getLimitedIntegerMaskConfig(limit = Constants.validDigitsLimitInInt): TextMask {
    return {
        ...unprefixedRawNumberConfig,
        allowDecimal: false,
        integerLimit: limit,
    };
}

function getLimitedDecimalMaskConfig(
    integerPartLimit = Constants.validDigitsLimitInInt,
    decimalPartLimit = Constants.defaultDecimalDigitsLimit
): TextMask {
    return {
        ...unprefixedRawNumberConfig,
        allowDecimal: true,
        integerLimit: integerPartLimit,
        decimalLimit: decimalPartLimit,
    };
}

export const inputMasks = {
    percentMask: createNumberMask({
        ...getLimitedDecimalMaskConfig(Constants.percentsMaxValue.toString().length),
        suffix: "%",
        allowNegative: false,
    }),

    integerPercentMask: createNumberMask({
        ...getLimitedIntegerMaskConfig(Constants.percentsMaxValue.toString().length),
        suffix: "%",
        allowNegative: false,
    }),

    dollarMask: createNumberMask({
        ...getLimitedDecimalMaskConfig(),
        prefix: "$",
    }),

    phoneMask: {
        textMask: ["+", /\d/, /\d/, /\d/, " ", "(", /\d/, /\d/, ")", /\d/, /\d/, /\d/, "-", /\d/, /\d/, "-", /\d/, /\d/],
        getRawValue: value => value.replace(/\D+/g, "")
    } as InputMask,
};

export function getLimitedIntegerMask(limit?: number): InputMask {
    return createNumberMask({
        ...getLimitedIntegerMaskConfig(limit),
        allowNegative: true,
    });
}

export function getLimitedDigitsMask(limit = Constants.fieldMaxLengths.defaultText): InputMask {
    return createNumberMask({
        ...getLimitedIntegerMaskConfig(limit),
        allowLeadingZeroes: true,
    });
}

export function getLimitedDecimalMask(integerPartLimit?: number, decimalPartLimit?: number): InputMask {
    return createNumberMask({
        ...getLimitedDecimalMaskConfig(integerPartLimit, decimalPartLimit)
    });
}

export function getCurrencyMask(integerPartLimit?: number, hideCurrencyPrefix = false, allowNegative = false): InputMask {
    return createNumberMask({
        ...getLimitedDecimalMaskConfig(integerPartLimit),
        suffix: hideCurrencyPrefix ? "" : " руб.",
        allowNegative,
    });
}