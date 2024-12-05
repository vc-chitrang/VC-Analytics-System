using UnityEngine;
using System;
using System.Collections.Generic;
using System.Globalization;

[Serializable]
public class SessionInformation {
    public string SessionId;
    public string SessionDuration;     

    public SessionInformation() {
       SessionId = Guid.NewGuid().ToString();
       SessionDuration = DateTime.MinValue.ToString();
    }
}

public class VCSessionData: MonoBehaviour {    
    //Session ID: Unique identifier for each session to track user journeys. 
    //Session Start and End Time: To measure session duration and time-on-page. 
    //Session Duration: Total time a user spends per session. 
    //Session Count: The number of sessions a user has, which helps track user engagement and retention.     
    private long startTime;
    private long endTime;
    private SessionInformation sessionInformation;

    private void Start() {        
        sessionInformation = new SessionInformation();            
        TrackSessionStart();
    }

    private void OnApplicationQuit() {
        TrackSessionEnd(); 
    }

    private void OnApplicationPause(bool pauseStatus) {
        if (pauseStatus) {
            TrackSessionEnd();
        }
        else {            
            TrackSessionStart();
        }
    }

    private void TrackSessionStart() {
        startTime = DateTime.Now.Ticks;
        sessionInformation = new SessionInformation();
    }

    private void TrackSessionEnd()
    {
        endTime = DateTime.Now.Ticks;

        long screenUpTimeDuration = endTime - startTime;  

        DateTime logTime = DateTime.ParseExact(sessionInformation.SessionDuration, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);

        DateTime totalDuration = logTime.AddTicks(screenUpTimeDuration);
        //if totalDuration is less then 5min then 
        sessionInformation.SessionDuration = totalDuration.ToString();

        UserInteractionTracker tracker = new UserInteractionTracker(Constant.SESSION_INFO_CONST); 
        AnalyticsEvent totalEventData = tracker.Create(new Dictionary<string, object>() {
            { sessionInformation.SessionId, sessionInformation },            
        });

        AnalyticsManager.Instance.AddOrUpdateParams(totalEventData, $"{sessionInformation.SessionId}");                
        AnalyticsManager.Instance.StoreData();
    }
}
