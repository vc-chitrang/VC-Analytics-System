using UnityEngine;
using System;
using System.Globalization;
using System.Collections.Generic;

//It is used to track the time a screen is viewed (int: count) and the total time spent on a screen (DateTime: "dd-MM-yyyy HH:mm:ss").
public class ScreenUpTimeTracker : MonoBehaviour {
    private long startTime;
    private long endTime;
    void OnEnable() {
        TrackSessionStart();
    }

    void OnDisable() {
        TrackSessionEnd(); 
    }

    private void TrackSessionStart()
    {
        startTime = DateTime.Now.Ticks;
    }

    private void TrackSessionEnd()
    {
        endTime = DateTime.Now.Ticks;
        string dateString = (string)AnalyticsManager.Instance.GetParameterData("Screen_View", gameObject.name);
        if(string.IsNullOrEmpty(dateString)) {
            dateString = DateTime.MinValue.ToString();
            Debug.Log(dateString);
        }
        long screenUpTimeDuration = endTime - startTime;     
        DateTime logTime = DateTime.ParseExact(dateString, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);

        DateTime totalDuration = logTime.AddTicks(screenUpTimeDuration);

        UserInteractionTracker tracker = new UserInteractionTracker("Screen_View"); 
        AnalyticsEvent totalEventData = tracker.Create(new Dictionary<string, object>() {
            { $"{gameObject.name}", totalDuration.ToString() }
        });
        AnalyticsManager.Instance.AddOrUpdateParams(totalEventData, $"{gameObject.name}");                
        AnalyticsManager.Instance.StoreData();
    }
}//ScreenUpTimeTracker class end.
