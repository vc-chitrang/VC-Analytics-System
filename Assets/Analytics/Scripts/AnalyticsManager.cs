using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

[Serializable]
public class AnalyticsEvent {
	public string EventName;
	public Dictionary<string, object> Parameters;
	public DateTime Timestamp;
	public AnalyticsEvent(string eventName, Dictionary<string, object> parameters) {
		EventName = eventName;
		Parameters = parameters;
		Timestamp = DateTime.UtcNow;
	}
}

public class AnalyticsManager : MonoBehaviour {
	private static AnalyticsManager _instance;
	public static AnalyticsManager Instance => _instance;
	private string offlineStoragePath;

	private void Awake() {
		if (_instance != null && _instance != this) {
			Destroy(this.gameObject);
			return;
		}
		_instance = this;
		DontDestroyOnLoad(this.gameObject);
		Initialize();
	}

	// Initialize any required analytics data
	public void Initialize() {
		// Load or set up default values, user identifiers, etc.
		offlineStoragePath = Path.Combine(Application.persistentDataPath, "AnalyticsData.json");
	}

	public void TrackEvent(string eventName, Dictionary<string, object> eventParams) {
		AnalyticsEvent newEvent = new AnalyticsEvent(eventName, eventParams);
		string json = JsonConvert.SerializeObject(newEvent);
		File.WriteAllText(offlineStoragePath, json);
		Debug.Log($"STORAGE: {offlineStoragePath}");
	}
}// AnalyticsManager class end.

