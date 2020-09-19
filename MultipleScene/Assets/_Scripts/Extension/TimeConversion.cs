using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeConversion
{
    public static string SecondsConverToText(float seconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);

        string timeText;

        if(timeSpan.Days > 0)
        {
            timeText = string.Format("{0} days, {1:D2}:{2:D2}:{3:D2}", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }
        else if(timeSpan.Hours > 0)
        {
            timeText = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }
        else
        {
            timeText = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes + timeSpan.Hours * 60, timeSpan.Seconds);
        }

        return timeText;
    }
}
