using UnityEngine;

public class Examples : MonoBehaviour {
	void Start() {
		AnalyticsEvent _analyticalData = new UserDemographicProfileTracker().Create();
		AnalyticsManager.Instance.StoreEvent(_analyticalData);
	}
}
