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

		//DebugEvents(_analysisDataDict);
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
			_e.Timestamp = eventData.Timestamp;
			Dictionary<string, object> _parameters = _e.Parameters;

			if (_parameters.ContainsKey(parameterKey)) {
				//already contains value of the parameter so update existing event value.
				_parameters[parameterKey] = eventData.Parameters[parameterKey];
			} else {
				_parameters.Add(parameterKey, eventData.Parameters[parameterKey]);
			}
		} else {
			_analysisDataDict.Add(eventData.EventName, eventData);
		}
	}

	public object GetParameterData(string eventNameKey, string parameterKey) {
		// Check if the main dictionary contains the event name key
		if (_analysisDataDict.ContainsKey(eventNameKey)) {
			// Check if the Parameters dictionary within the event contains the parameter key
			if (_analysisDataDict[eventNameKey].Parameters.ContainsKey(parameterKey)) {
				return _analysisDataDict[eventNameKey].Parameters[parameterKey];
			}
		}

		// Return null or an appropriate default value if keys are not found
		return null; // or a default value as per your requirement
	}

	public void StoreData() {
		string json = JsonConvert.SerializeObject(_analysisDataDict.Values,Formatting.Indented);
		File.WriteAllText(offlineStoragePath, json);
	}
}// AnalyticsManager class end.

