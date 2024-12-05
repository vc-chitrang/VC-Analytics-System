using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionButtonUI : MonoBehaviour {
	public DataUIBase panel;
	[HideInInspector] public string key;
	private AnalyticsReader reader;
	private AnalyticsEvent data;
	[SerializeField] private Button button;
	[SerializeField] private TextMeshProUGUI _buttonText;

	void Awake() {
		if (button is null) {
			button = GetComponent<Button>();
		}
		button.onClick.RemoveAllListeners();
		button.onClick.AddListener(OnClick);

		this.key = name.ToLower().Trim();

		string displayName = StringUtility.ReplaceUnderScoreAndDashWithSpace(name);
		displayName = StringUtility.ConvertToTitleCase(displayName);
		_buttonText.text = displayName;
	}

	public void Init(AnalyticsReader reader, AnalyticsEvent data) {
		this.data = data;
		this.reader = reader;

		panel.Init(data);
	}

	private void OnClick() {
		if (reader is null) {
			return;
		}
		reader.DisableAllPanels();
		panel.SetPanel(true);
	}
}//SelectionButtonUI class end.
