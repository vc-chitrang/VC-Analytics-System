using System.Collections.Generic;
using UnityEngine;

public class Examples : MonoBehaviour {
	void Start() {
		// Example: Tracking level completion
		Dictionary<string, object> parameters = new Dictionary<string, object>
		{
			{ "levelNumber", 3 },
			{ "timeSpent", 120 } // seconds
		};		

		AnalyticsManager.Instance.TrackEvent("LevelCompleted", parameters);
	}
}
