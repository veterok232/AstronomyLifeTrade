import MessagesEn from "../../../static/localization/common.en.json";
import flatten from "flat";
import { merge } from "lodash";
import { ReactElement, ReactNode } from "react";
import { createIntl, createIntlCache, IntlShape, PrimitiveType } from "react-intl";
import { getEnumLocalizationKey } from "./enumRegistrator";
import { Constants } from "../constants";
import { parseDateTime } from "./dateTimeParser";

function getZeroValue(precision = 0): string {
    if (!precision) {
        return "0";
    }
    return `0.${"0".repeat(precision)}`;
}

const translations = {
    "en": flatten(MessagesEn),
};

export class Localizer {
    public static instance = new Localizer();

    public static configureIntl(locale?: string): IntlShape {
        const localeToSet = locale || navigator.language.split(/[-_]/)[0];
        const supportedLocale = localeToSet === "en" || localeToSet === "es" ? localeToSet : "en";

        this.intl = createIntl({
            locale: supportedLocale,
            messages: merge(
                {},
                translations[supportedLocale],
            ),
        }, createIntlCache());

        return this.intl;
    }

    private static intl: IntlShape;

    public get(id: string, values?: Record<string, PrimitiveType>): string {
        return Localizer.intl && Localizer.intl.formatMessage({ id }, values);
    }

    public getWithMarkup(id: string, values?: Record<string, ReactElement>): string | ReactNode {
        return Localizer.intl && Localizer.intl.formatMessage({ id }, values);
    }

    public getEnumValue<TEnum, TEnumValue>(enumInstance: TEnum, enumValue: TEnumValue, values?: Record<string, PrimitiveType>): string {
        return this.get(getEnumLocalizationKey(enumInstance, enumValue), values);
    }

    public formatDate(date: Date): string {
        const parsedDateTime = this.getParsedDateTime(date);

        return parsedDateTime && Localizer.intl.formatDate(parsedDateTime);
    }

    public formatTime(dateTime: Date): string {
        const parsedDateTime = this.getParsedDateTime(dateTime);

        return parsedDateTime && `${Localizer.intl.formatTime(parsedDateTime)}`;
    }

    public formatDateTime(dateTime: Date): string {
        const parsedDateTime = this.getParsedDateTime(dateTime);

        return parsedDateTime && `${Localizer.intl.formatDate(parsedDateTime)} ${Localizer.intl.formatTime(parsedDateTime)}`;
    }

    public formatDayMonth(date: Date): string {
        return Localizer.intl.formatDate(date, { day: "numeric", month: "numeric" });
    }

    public formatTimeInMonths = (value: number): string => {
        const years = Math.trunc(value / Constants.monthsInYear);
        const months = value - years * Constants.monthsInYear;

        if (years > 0 && months > 0) {
            return this.get("TimeInMonthsFormat_YearsMonthsTemplate", { years: years, months: months });
        } else if (years > 0) {
            return this.get("TimeInMonthsFormat_YearsTemplate", { years: years });
        } else if (months > 0) {
            return this.get("TimeInMonthsFormat_MonthsTemplate", { months: months });
        } else {
            return Constants.defaultEmptyValue;
        }
    };

    public formatNumber(number: number, precision?: number, useGrouping?: boolean): string {
        return number ? Localizer.intl.formatNumber(number, {
            maximumFractionDigits: precision,
            minimumFractionDigits: precision,
            useGrouping: useGrouping ?? false,
        }) : getZeroValue(precision);
    }

    public formatMoney(number: number, useGrouping?: boolean): string {
        return this.formatNumber(number, Constants.moneyPrecision, useGrouping);
    }

    public formatPercents(number: number, precision: number = Constants.defaultDecimalPrecision): string {
        return this.formatNumber(number, precision);
    }

    public getWeekdayName(date: Date): string {
        return Localizer.intl.formatDate(date, { weekday: "long" });
    }

    private getParsedDateTime(dateTime: Date | string): Date {
        return (dateTime && dateTime.toString() !== "Invalid Date")
            ? parseDateTime(dateTime)
            : null;
    }
}

export const localizer = Localizer.instance;