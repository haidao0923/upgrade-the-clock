using System;
using UnityEngine;

/*public enum TimeUnit
{
    SECOND = 1,
    MINUTE = 60 * SECOND,
    HOUR = 60 * MINUTE,
    DAY = 24 * HOUR,
    YEAR = 365 * DAY,
    EPOCH_MULTIPLIER = 1000000,
}*/

public class TimeFunction
{
    public static class TimeUnit
    {
        public static double second = 1;
        public static double minute = 60 * second;
        public static double hour = 60 * minute;
        public static double day = 24 * hour;
        public static double year = 365 * day;
        public static double epoch = 1000000 * year;
    }

    public static string ConvertValueToString(double value)
    {
        double epoch = GetEpoch(value);
        double year = GetYear(value);
        double day = GetDay(value);
        double hour = GetHour(value);
        double minute = GetMinute(value);
        double second = GetSecond(value);

        if (epoch >= 1)
        { return epoch + "e" + year + "y" + day + "d" + hour + "h" + minute + "m" + second + "s"; }
        else if (year >= 1)
        { return year + "y" + day + "d" + hour + "h" + minute + "m" + second + "s"; }
        else if (day >= 1)
        { return day + "d" + hour + "h" + minute + "m" + second + "s"; }
        else if (hour >= 1)
        { return hour + "h" + minute + "m" + second + "s"; }
        else if (minute >= 1)
        { return minute + "m" + second + "s"; }
        else { return second + "s"; }
    }

    public static string ConvertValueToTimerDisplay(int value)
    {
        int hour = value / (int)TimeUnit.hour;
        int minute = value % (int)TimeUnit.hour / (int)TimeUnit.minute;
        int second = value % (int)TimeUnit.minute;
        return hour.ToString("00") + ":" + minute.ToString("00") + ":" + second.ToString("00");
    }

    public static void Countdown(ref int value, int decrement = 1)
    {
        if (value > 0)
        {
            value -= decrement;
        }
    }

    public static void Countup(ref int value, int maxValue, int increment = 1)
    {
        if (increment < 1)
        {
            increment = 1;
        }
        if (value < maxValue)
        {
            value += increment;
        }
        if (value > maxValue)
        {
            value = maxValue;
        }
    }

    public static double GetEpoch(double value)
    {
        double epoch = Math.Floor(value / TimeUnit.epoch);
        return epoch;
    }

    public static double GetYear(double value)
    {
        double year = Math.Floor(value % TimeUnit.epoch / TimeUnit.year);
        return year;
    }

    public static double GetDay(double value)
    {
        double day = Math.Floor(value % TimeUnit.year / TimeUnit.day);
        return day;
    }

    public static double GetHour(double value)
    {
        double hour = Math.Floor(value % TimeUnit.day / TimeUnit.hour);
        return hour;
    }

    public static double GetMinute(double value)
    {
        double minute = Math.Floor(value % TimeUnit.hour / TimeUnit.minute);
        return minute;
    }

    public static double GetSecond(double value)
    {
        double second = Math.Floor(value % TimeUnit.minute);
        return second;
    }
}
