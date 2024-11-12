using System;
using System.Diagnostics;
using UnityEngine.UI;

public class VCButton : Button {
	private UserInteractionTracker _tracker;
	protected override void Awake() {
		_tracker = new UserInteractionTracker("Button_Click");
		UnityEngine.Debug.Log($"{name} : {_tracker.EventName}");
		onClick.AddListener(() => {
			string parameterKey = $"{this.gameObject.name}";
			object data = AnalyticsManager.Instance.GetParameterData(_tracker.EventName, parameterKey);
			int intValue = Convert.ToInt32((data is null) ? 0 : data);
			UnityEngine.Debug.Log($"DATA: {intValue}");
			_tracker.AddButtonClickCount(parameterKey, (intValue + 1));
			AnalyticsEvent _eventData = _tracker.Create();

			AnalyticsManager.Instance.AddOrUpdateParams(_eventData, parameterKey);
			AnalyticsManager.Instance.StoreData();
		});
	}
}
