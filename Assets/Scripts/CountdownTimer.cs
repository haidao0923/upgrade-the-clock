using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public static bool Countdown(int[] timer)
    {
        FixUpperBound(timer);
        if (timer[2] > 0)
        { timer[2] -= 1; }
        else if (timer[2] == 0 && timer[1] > 0)
        { timer[1] -= 1; timer[2] += 59; }
        else if (timer[2] == 0 && timer[1] == 0 && timer[0] > 0)
        { timer[0] -= 1; timer[1] += 59; timer[2] += 59; }
        else
        {
            return false;
        }
        return true;
    }

    public static bool Countup(int[] timer, int[] upperbound, int increment)
    {
        if (CompareTime(timer, upperbound) > 0)
        {
            timer = upperbound;
            return false;
        }

        FixUpperBound(timer);
        if (increment == 0)
        {
            increment = 1;
        }
        timer[2] += increment;

        if (timer[2] >= 60)
        {
            timer[1] += timer[2] / 60;
            timer[2] = timer[2] % 60;
        }
        if (timer[1] >= 60)
        {
            timer[0] += timer[1] / 60;
            timer[1] = timer[1] % 60;
        }

        return true;
    }

    public static void FixUpperBound(int[] timer)
    {
        if (timer[2] >= 60)
        {
            timer[1] += timer[2] / 60;
            timer[2] = timer[2] % 60;
        }
        if (timer[1] >= 60)
        {
            timer[0] += timer[1] / 60;
            timer[1] = timer[1] % 60;
        }
    }

    public static int CompareTime(int[] timer1, int[] timer2)
    {
        return ConvertToSeconds(timer1) - ConvertToSeconds(timer2);
    }

    public static int ConvertToSeconds(int[] timer)
    {
        return timer[0] * 3600 + timer[1] * 60 + timer[2];
    }

    public static int[] ConvertToArray(int seconds)
    {
        int hour = seconds / 3600;
        int minute = (seconds % 3600) / 60;
        int second = seconds % 60;
        return new int[] { hour, minute, second };
    }

    public static string ToString(int[] timer)
    {
        return timer[0].ToString("00") + ":" + timer[1].ToString("00") + ":" + timer[2].ToString("00");
    }
}
