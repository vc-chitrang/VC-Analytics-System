using System.Collections.Generic;
using UnityEngine;

public class DeviceandTechnicalInformationTracker : TrackerBase, ITracker
{
    public string EventName => "Device-and-Technical-Information";

    public AnalyticsEvent Create()
    {
        Dictionary<string, object> eventParams = new Dictionary<string, object>();
        //Device Model: Device model (e.g., "iPhone 12" or "Samsung Galaxy S21"). 
        // Operating System: OS and version (e.g., Android 11, iOS 14.4). 
        // App Version: Version of the app (e.g., "1.2.0") to analyze user behavior across versions. 
        // Screen Resolution: Helps analyze usability based on screen size. 
        // Network Type: Connectivity information (e.g., Wi-Fi, 4G, 5G). 
        // Battery Level: If applicable, tracking battery could help analyze drop-offs. 
        eventParams.Add("Device Model", SystemInfo.deviceModel);
        eventParams.Add("Operating System", SystemInfo.operatingSystem);
        eventParams.Add("DeviceName", SystemInfo.deviceName);
        eventParams.Add("Company Name", Application.companyName);
        eventParams.Add("Product Name", Application.productName);
        eventParams.Add("App Version", Application.version);
        eventParams.Add("Screen Resolution", Screen.width + "x" + Screen.height);
        eventParams.Add("Network Type", Application.internetReachability.ToString());
        eventParams.Add("Battery Level", SystemInfo.batteryLevel >= 0 ? SystemInfo.batteryLevel.ToString() : "Not Applicable");

        return new TrackerFactory().CreateTracker(EventName, eventParams);
    }
}
