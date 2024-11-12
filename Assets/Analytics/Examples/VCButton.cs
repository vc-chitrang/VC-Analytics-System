using UnityEngine.UI;

public class VCButton : Button {
	private UserInteractionTracker _tracker;
	protected override void Awake() {
		_tracker = new UserInteractionTracker("Button_Click");
		onClick.AddListener(() => {
			string parameterKey = $"{this.gameObject.name}";
			_tracker.AddButtonClickCount(parameterKey);
			AnalyticsEvent _eventData = _tracker.Create();			

			AnalyticsManager.Instance.AddOrUpdateParams(_eventData, parameterKey);
			AnalyticsManager.Instance.StoreData();
		});
	}
}
