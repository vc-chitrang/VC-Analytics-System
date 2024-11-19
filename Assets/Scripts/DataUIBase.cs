using TMPro;
using UnityEngine;

public class DataUIBase : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private GameObject panel;
    private void Start()
    {
        SetPanel(false);
    }
    public void SetPanel(bool isEnable)
    {
        panel.SetActive(isEnable);
    }

    public virtual void Init(AnalyticsEvent data)
    {
        // Debug.Log($"Key: {data.EventName}");
        string displayName = StringUtility.ReplaceSpaceWithUnderscore(data.EventName);
        name = _title.text = StringUtility.ConvertToTitleCase(displayName);
    }
}
