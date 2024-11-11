using UnityEngine;
using UnityEngine.UI;
public class VCButton : Button {
	private UserInteractionTracker tracker;
	protected override void Awake() {
		tracker = new UserInteractionTracker("Button_Click");
		onClick.AddListener(() => {
			tracker.Add($"{this.gameObject.name}",UnityEngine.Random.Range(0,100));
			AnalyticsEvent _event = tracker.Create();
			Debug.Log($"{JsonUtility.ToJson(_event)}");
			AnalyticsManager.Instance.StoreEvent(_event);
		});
	}
}
