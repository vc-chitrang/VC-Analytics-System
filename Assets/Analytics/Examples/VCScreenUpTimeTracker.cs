using UnityEngine;
using System;
using System.Globalization;
using System.Collections.Generic;
using Newtonsoft.Json;

[System.Serializable]
public class ScreenTimeData {
    public string Duration;
    public int ViewCount;   

    public ScreenTimeData() {
        Duration = DateTime.MinValue.ToString();
        ViewCount = 0;
    }
}
//It is used to track the time a screen is viewed (int: count) and the total time spent on a screen (DateTime: "dd-MM-yyyy HH:mm:ss").
public class VCScreenUpTimeTracker : MonoBehaviour {
    private long startTime;
    private long endTime;
    void OnEnable() {
        TrackSessionStart();
    }

    void OnDisable() {
        TrackSessionEnd(); 
    }

    private void TrackSessionStart() {
        startTime = DateTime.Now.Ticks;
    }

    private void TrackSessionEnd() { 
        endTime = DateTime.Now.Ticks;

        object existingData = AnalyticsManager.Instance.GetParameterData("Screen_View", gameObject.name);        
        ScreenTimeData screenTimeData = JsonConvert.DeserializeObject<ScreenTimeData>(JsonConvert.SerializeObject(existingData));

        if(screenTimeData is null) {
            screenTimeData = new ScreenTimeData();
        }

        long screenUpTimeDuration = endTime - startTime;     
        DateTime logTime = DateTime.ParseExact(screenTimeData.Duration, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);

        DateTime totalDuration = logTime.AddTicks(screenUpTimeDuration);

        UserInteractionTracker tracker = new UserInteractionTracker("Screen_View"); 
        AnalyticsEvent totalEventData = tracker.Create(new Dictionary<string, object>() {
            { gameObject.name, new ScreenTimeData() { Duration = totalDuration.ToString(), ViewCount = screenTimeData.ViewCount + 1} },            
        });

        AnalyticsManager.Instance.AddOrUpdateParams(totalEventData, $"{gameObject.name}");                
        AnalyticsManager.Instance.StoreData();
    }
}//ScreenUpTimeTracker class end.
