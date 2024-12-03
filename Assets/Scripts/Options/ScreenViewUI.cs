using Newtonsoft.Json;
using System;
using UnityEngine;

public class ScreenViewUI:DataUIBase {
	[SerializeField] private ReusableScrollPanel reusableScrollPanel;

	public override void Init(AnalyticsEvent data) {
		base.Init(data);

		foreach (var i in data.Parameters) {
			ScreenTimeData _screenTimeData = JsonConvert.DeserializeObject<ScreenTimeData>(i.Value.ToString());
			string screenName = i.Key;
			string duration = _screenTimeData.Duration;
			int viewCount = _screenTimeData.ViewCount;

			// Format the duration using TimeSpan for better readability
			DateTime time = DateTime.ParseExact(duration,"dd-MM-yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture);
			TimeSpan sessionDuration = time.TimeOfDay;

			// Construct the human-readable message
			string _message = $"The screen <b>'{screenName}'</b> was viewed <b>{viewCount}</b> times, with a total duration of <b>{sessionDuration.Hours:D2}H:{sessionDuration.Minutes:D2}m:{sessionDuration.Seconds:D2}s.</b>";

			reusableScrollPanel.Display(_message);
		}
	}
}//ScreenViewUI class end.
