using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

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

	private string offlineStoragePath { get => Path.Combine(Application.persistentDataPath, "AnalyticsData.json"); }
	private Dictionary<string,AnalyticsEvent> _analysisDataDict = new Dictionary<string, AnalyticsEvent>();

	private async void Awake() {
		Singletone();
		await RetriveData(OnDataRetrived);		
	}

	private void Singletone() {
		if (_instance != null && _instance != this) {
			Destroy(this.gameObject);
			return;
		}
		_instance = this;
		DontDestroyOnLoad(this.gameObject);
	}

	private async Task RetriveData(Action<Dictionary<string, AnalyticsEvent>> callback) {
		string json = await File.ReadAllTextAsync(offlineStoragePath);
		Dictionary<string,AnalyticsEvent> _retriveData = DeserializeToDictionary(json);
		callback?.Invoke(_retriveData);
	}

	private void OnDataRetrived(Dictionary<string, AnalyticsEvent> retriveData) {
		_analysisDataDict.Clear();
		_analysisDataDict = retriveData;
		
		AddEventData(new UserDemographicProfileTracker().Create());
		StoreData();

		DebugEvents(_analysisDataDict);
	}

	public Dictionary<string, AnalyticsEvent> DeserializeToDictionary(string json) {
		if (string.IsNullOrWhiteSpace(json)) {
			return new Dictionary<string, AnalyticsEvent>();
		}
		// Deserialize and immediately convert to Dictionary using LINQ
		return JsonConvert.DeserializeObject<List<AnalyticsEvent>>(json)
			.Where(e => e != null && !string.IsNullOrEmpty(e.EventName)) // Filter null or invalid entries
			.ToDictionary(e => e.EventName); // Convert to Dictionary with EventName as key
	}

	private static void DebugEvents(Dictionary<string, AnalyticsEvent> analyticsEvents) {
		foreach (var kvp in analyticsEvents) {
			string key = kvp.Key;
			AnalyticsEvent analyticsEvent = kvp.Value;

			Debug.Log($"Key: {key}");
			Debug.Log($"Event Name: {analyticsEvent.EventName}");
			Debug.Log($"Timestamp: {analyticsEvent.Timestamp}");

			foreach (var param in analyticsEvent.Parameters) {
				Debug.Log($"Parameter: {param.Key} = {param.Value}");
			}
		}
	}
	public void StoreEvent(string eventName, Dictionary<string, object> eventParams) {
		AnalyticsEvent newEvent = new AnalyticsEvent(eventName, eventParams);
		string json = JsonConvert.SerializeObject(newEvent);
		File.WriteAllText(offlineStoragePath, json);
		Debug.Log($"STORAGE: {offlineStoragePath}");
	}

	public void StoreEvent(AnalyticsEvent analyticsEvent) {
		if (_analysisDataDict.ContainsKey(analyticsEvent.EventName)) {
			_analysisDataDict[analyticsEvent.EventName] = analyticsEvent;
		} else {
			_analysisDataDict.Add(analyticsEvent.EventName, analyticsEvent);
		}
		string json = JsonConvert.SerializeObject(_analysisDataDict.Values);
		File.WriteAllText(offlineStoragePath, json);
		Debug.Log($"STORAGE: {offlineStoragePath}");
	}

	public void AddEventData(AnalyticsEvent eventData) {
		if (_analysisDataDict.ContainsKey(eventData.EventName)) {		
			_analysisDataDict[eventData.EventName] = eventData;
		} else {
			_analysisDataDict.Add(eventData.EventName, eventData);
		}
	}

	public void AddOrUpdateParams(AnalyticsEvent eventData, string parameterKey) {
		if (string.IsNullOrWhiteSpace(parameterKey)) {
			return;
		}

		if (_analysisDataDict.ContainsKey(eventData.EventName)) {
			AnalyticsEvent _e = _analysisDataDict[eventData.EventName];
			Dictionary<string, object> _parameters = _e.Parameters;

			if (_parameters.ContainsKey(parameterKey)) {
				_parameters[parameterKey] = eventData.Parameters[parameterKey];
			} else {
				_parameters.Add(parameterKey, eventData.Parameters.Values);
			}
		} else {
			_analysisDataDict.Add(eventData.EventName, eventData);
		}
	}

	public void StoreData() {
		string json = JsonConvert.SerializeObject(_analysisDataDict.Values,Formatting.Indented);
		File.WriteAllText(offlineStoragePath, json);
		Debug.Log($"DATA Stored: {offlineStoragePath}");
	}
}// AnalyticsManager class end.

