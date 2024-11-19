using System.IO;
using UnityEngine;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;

public class AnalyticsReader : MonoBehaviour
{
    [SerializeField] private List<SelectionButtonUI> options;
    private List<AnalyticsEvent> analyticsEvents = new List<AnalyticsEvent>();

    private string offlineStoragePath { get => Path.Combine(Application.dataPath, "Resources", "AnalyticsData.json"); }

    private async void Awake()
    {
        await RetriveData(OnDataRetrived);
    }

    private async Task RetriveData(Action<string> callback)
    {
        try
        {
            if (File.Exists(offlineStoragePath))
            {
                string jsonData = await File.ReadAllTextAsync(offlineStoragePath);
                callback?.Invoke(jsonData);
            }
            else
            {
                Debug.LogWarning($"Analytics data file not found at: {offlineStoragePath}");
                callback?.Invoke(null);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error reading analytics data: {ex.Message}");
            callback?.Invoke(null);
        }
    }

    private void OnDataRetrived(string jsonData)
    {
        if (string.IsNullOrEmpty(jsonData))
        {
            Debug.LogError("ERROR: No analytics data available");
            return;
        }

        analyticsEvents = JsonConvert.DeserializeObject<List<AnalyticsEvent>>(jsonData);

        InitializeContentUI();
    }

    private void InitializeContentUI()
    {
        options.ForEach(o =>
        {
            AnalyticsEvent matchedEvent = analyticsEvents.Find(e =>
                e.EventName.Trim().Equals(o.key.Trim(), StringComparison.OrdinalIgnoreCase));

            if (matchedEvent is not null)
            {
                o.Init(this, matchedEvent);
            }
        });
    }

    public void DisableAllPanels()
    {
        options.ForEach(o => { o.panel.SetPanel(false); });
    }
}
