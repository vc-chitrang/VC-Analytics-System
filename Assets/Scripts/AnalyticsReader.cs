using System.IO;
using UnityEngine;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using TMPro;

public class AnalyticsReader : MonoBehaviour {

	[Header("UI")]
	[SerializeField] private TextMeshProUGUI _title;
	[SerializeField] private TextMeshProUGUI _appVersion;
	[SerializeField] private GameObject _browseFileScreen;

	[Header("-----------------------")]
	[SerializeField] private List<SelectionButtonUI> options;
	private List<AnalyticsEvent> analyticsEvents = new List<AnalyticsEvent>();

	private string offlineStoragePath {
		get => Path.Combine(Application.dataPath, "Resources", "AnalyticsData.json");
	}

	private async void Awake() {
		_title.text = $"{Application.companyName} Data Analytics Tool";
		_appVersion.text = $"{Application.productName}_v{Application.version}";

		_browseFileScreen.SetActive(true);
#if !UNITY_EDITOR_WIN
		if (File.Exists(offlineStoragePath)) {
			string jsonData = await File.ReadAllTextAsync(offlineStoragePath);
			OnDataRetrived(jsonData);
			_browseFileScreen.SetActive(false);
		}
#endif
	}

	private void OnEnable() {
#if UNITY_EDITOR_WIN
		JsonFileSelector.onJsonContentLoaded = OnJsonContentLoaded;
#endif
	}

	private void OnDisable() {
#if UNITY_EDITOR_WIN
		JsonFileSelector.onJsonContentLoaded -= OnJsonContentLoaded;
#endif
	}

	private void OnJsonContentLoaded(string jsonData) {
		OnDataRetrived(jsonData);
		_browseFileScreen.SetActive(false);
	}

	private void OnDataRetrived(string jsonData) {
		if (string.IsNullOrEmpty(jsonData)) {
			//TODO: Display Fail Popup
			Debug.LogError("ERROR: No analytics data available");
			return;
		}

		analyticsEvents = JsonConvert.DeserializeObject<List<AnalyticsEvent>>(jsonData);

		InitializeContentUI();
	}

	private void InitializeContentUI() {
		options.ForEach(o => {
			AnalyticsEvent matchedEvent = analyticsEvents.Find(e => e.EventName.Trim().Equals(o.key.Trim(), StringComparison.OrdinalIgnoreCase));

			if (matchedEvent is not null) {
				o.Init(this, matchedEvent);
			}
		});
	}

	public void DisableAllPanels() {
		options.ForEach(o => { o.panel.SetPanel(false); });
	}
}
