export const parseDateTime = (dateTime: string | Date): Date => {
    try {
        const [years, mounths, days, hours, minutes, seconds] =
            (dateTime as string).split(/[^0-9]/).map(x => parseInt(x));

        return new Date(years, mounths - 1, days, hours ?? 0, minutes ?? 0, seconds ?? 0);
    }
    catch {
        return dateTime as Date;
    }
};