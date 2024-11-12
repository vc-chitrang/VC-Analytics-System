using UnityEngine;
using UnityEngine.UI;
public class VCButton : Button {
	private UserInteractionTracker _tracker;
	protected override void Awake() {
		_tracker = new UserInteractionTracker("Button_Click");
		onClick.AddListener(() => {
			string parameterKey = $"{this.gameObject.name}";
			_tracker.Add(parameterKey,UnityEngine.Random.Range(0,100));
			AnalyticsEvent _event = _tracker.Create();
			Debug.Log($"{JsonUtility.ToJson(_event)}");

			AnalyticsManager.Instance.AddOrUpdateParams(_event, parameterKey);
			AnalyticsManager.Instance.StoreData();
		});
	}
}
