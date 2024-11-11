using UnityEngine;

public class Examples : MonoBehaviour {
	void Start() {
		AnalyticsManager.Instance.StoreEvent(new UserDemographicProfileTracker().Create());
	}

	public void OnCustomButtonClick() {
		Debug.Log("OnCustomButtonClick");
	}
}
