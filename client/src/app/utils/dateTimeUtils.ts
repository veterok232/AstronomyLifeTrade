const getDateOffsetMilliseconds = (date: Date): number => {
    return date.getTimezoneOffset() * 60000;
};

export const convertUtcDateToLocal = (date: Date) => {
    const correctedDate = new Date(date.getTime() - getDateOffsetMilliseconds(date));

    // slice applied to remove "Z" (UTC date symbol)
    return correctedDate.toISOString().slice(0, -1);
};

Date.prototype.toJSON = function () { return convertUtcDateToLocal(this as Date); };

export const getDate = (value: Date): Date => {
    value?.setHours(0, 0, 0, 0);
    return value;
};

export const convertUtcStringToDate = (value?: string): Date => {
    return value && new Date(value.endsWith("Z") ? value : `${value}Z`);
};

export const isDateInTheFuture = (date: Date) => {
    return Date.now() < new Date(date).getTime();
};

export const isDateInThePast = (date: Date) => {
    return getDate(new Date()) > new Date(date);
};

export const getDateYearsAgo = (years: number): Date => {
    const date = new Date();
    date.setFullYear(date.getFullYear() - years);

    return date;
};

export const isToday = (someDate: Date) => {
    const today = new Date();
    return someDate.getDate() === today.getDate() &&
        someDate.getMonth() === today.getMonth() &&
        someDate.getFullYear() === today.getFullYear();
};

export const compareDates = (value1: Date, value2: Date) => {
    const time1 = value1.getTime();
    const time2 = value2.getTime();

    return time1 == time2
        ? 0
        : time1 > time2 ? 1 : -1;
};

export const getTomorrowStartDate = () => {
    const date = new Date();
    date.setDate(date.getDate() + 1);

    return getDate(date);
};