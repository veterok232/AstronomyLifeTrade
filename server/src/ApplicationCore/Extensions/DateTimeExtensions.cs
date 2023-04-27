using ApplicationCore.Enums;

namespace ApplicationCore.Extensions;

internal static class DateTimeExtensions
{
    public static DateTime DayStart(this DateTime dateTime)
    {
        return dateTime.Date;
    }

    public static DateTime DayEnd(this DateTime dateTime)
    {
        return dateTime.DayStart().AddDays(1).AddTicks(-1);
    }

    public static DateTime MonthStart(this DateTime dateTime)
    {
        return dateTime.AddDays(1 - dateTime.Day).DayStart();
    }

    public static DateTime MonthEnd(this DateTime dateTime)
    {
        return dateTime.MonthStart().AddMonths(1).AddTicks(-1);
    }

    public static DateTime YearStart(this DateTime dateTime)
    {
        return dateTime.AddDays(1 - dateTime.DayOfYear).DayStart();
    }

    public static DateTime YearEnd(this DateTime dateTime)
    {
        return dateTime.YearStart().AddYears(1).AddTicks(-1);
    }

    public static DateTime StartOfRange(this DateTime date, DateRangeType rangeType)
    {
        return rangeType switch
        {
            DateRangeType.Today => date.DayStart(),
            DateRangeType.Yesterday => date.AddDays(-1).DayStart(),
            DateRangeType.ThisWeek => date.AddDays(-6).DayStart(),
            DateRangeType.LastWeek => date.AddDays(-13).DayStart(),
            DateRangeType.ThisMonth => date.MonthStart(),
            DateRangeType.LastMonth => date.MonthStart().AddMonths(-1),
            DateRangeType.ThisQuarter => GetQuarterStart(date),
            DateRangeType.LastQuarter => GetQuarterStart(date).AddMonths(-3),
            DateRangeType.ThisYear => date.YearStart(),
            DateRangeType.LastYear => date.YearStart().AddYears(-1),
            _ => throw new ArgumentException($"Unable to get start of range for {rangeType} type"),
        };
    }

    public static DateTime EndOfRange(this DateTime date, DateRangeType rangeType)
    {
        return rangeType switch
        {
            DateRangeType.Today => date.DayEnd(),
            DateRangeType.Yesterday => date.AddDays(-1).DayEnd(),
            DateRangeType.ThisWeek => date.DayEnd(),
            DateRangeType.LastWeek => date.DayEnd().AddDays(-7),
            DateRangeType.ThisMonth => date.MonthEnd(),
            DateRangeType.LastMonth => date.AddMonths(-1).MonthEnd(),
            DateRangeType.ThisQuarter => GetQuarterEnd(date),
            DateRangeType.LastQuarter => GetQuarterEnd(date.AddMonths(-3)),
            DateRangeType.ThisYear => date.YearEnd(),
            DateRangeType.LastYear => date.YearEnd().AddYears(-1),
            _ => throw new ArgumentException($"Unable to get end of range for {rangeType} type"),
        };
    }

    private static DateTime GetQuarterStart(DateTime dateTime)
    {
        var quarterFirstMonth = (((dateTime.Month - 1) / 3) * 3) + 1;

        return new DateTime(dateTime.Year, quarterFirstMonth, day: 1, hour: 0, minute: 0, second: 0, dateTime.Kind);
    }

    private static DateTime GetQuarterEnd(DateTime dateTime)
    {
        var quarterStart = GetQuarterStart(dateTime);

        return quarterStart.AddMonths(3).AddDays(-1).DayEnd();
    }
}