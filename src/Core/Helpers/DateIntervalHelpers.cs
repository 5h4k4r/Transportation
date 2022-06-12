namespace Core.Helpers;

public static class DateIntervalHelpers
{
    public static double CalculateHoursExcludingRange(
        DateTime startDate,
        DateTime endDate,
        DateTime rangeStart,
        DateTime rangeEnd)

    {
        startDate = new DateTime(1970, 1, 1, startDate.Hour, startDate.Minute, startDate.Second);
        endDate = new DateTime(1970, 1, 1, endDate.Hour, endDate.Minute, endDate.Second);
        rangeStart = new DateTime(1970, 1, 1, rangeStart.Hour, rangeStart.Minute, rangeStart.Second).ToUniversalTime();
        rangeEnd = new DateTime(1970, 1, 1, rangeEnd.Hour, rangeEnd.Minute, rangeEnd.Second).ToUniversalTime();


        var loopTimes = 2;
        if (startDate > endDate)
        {
            endDate = endDate.AddDays(1);
            loopTimes++;
        }

        if (rangeStart > rangeEnd) rangeEnd = rangeEnd.AddDays(1);

        var interval = new Interval(startDate, endDate);
        var totalHours = interval.durationInSeconds() / 3600;
        for (var i = 0; i < loopTimes; i++)
        {
            var excludingRange = new Interval(rangeStart.AddDays(i), rangeEnd.AddDays(i));

            if (excludingRange.contains(interval)) return 0;

            if (interval.contains(excludingRange))
            {
                totalHours -= excludingRange.durationInSeconds() / 3600;
            }
            else if (interval.cross(excludingRange))
            {
                var intersection = interval.intersection(excludingRange);
                totalHours -= intersection.durationInSeconds() / 3600;
            }
        }

        return totalHours;
    }
}

internal class Interval
{
    private readonly DateTime _end;
    private readonly DateTime _start;

    public Interval(DateTime start, DateTime end)
    {
        _start = start;
        _end = end;
    }

    public double durationInSeconds()
    {
        return _end.Subtract(_start).TotalSeconds;
    }

    public bool cross(Interval other)
    {
        return includes(other._start) || includes(other._end);
    }

    public bool contains(Interval interval)
    {
        return includes(interval._start) && includes(interval._end);
    }


    public bool includes(DateTime date)
    {
        return date >= _start && date <= _end;
    }

    public Interval intersection(Interval other)
    {
        if (cross(other))
            return _start >= other._start
                ? new Interval(_start, other._end)
                : new Interval(other._start, _end);

        throw new Exception();
    }

    public override string ToString()
    {
        return $"{_start} => {_end} : {durationInSeconds()}";
    }
}