using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class SessionInformationUI:DataUIBase {
	[SerializeField] private TextMeshProUGUI _sessionInfo;

	public override void Init(AnalyticsEvent data) {
		base.Init(data);

		TimeDurationCalculator calculator = new TimeDurationCalculator();

		foreach (var i in data.Parameters) {
			SessionInformation session = JsonConvert.DeserializeObject<SessionInformation>(i.Value.ToString());
			calculator.AddDuration(session.SessionDuration);
		}
		_sessionInfo.text = string.Empty;

		//number of sessions count.
		_sessionInfo.text += $"<b><color=yellow>{data.Parameters.Count}</color></b> Time the app were opened,\n" ;

		//Total Session Duration Time.
		_sessionInfo.text += _sessionInfo.text = $"Total <b><color=yellow>{calculator.GetTotalDuration()}</color></b> of usage time,\n";

		//Average Session Up Time.
		_sessionInfo.text += _sessionInfo.text = $"with an average session duration of <b><color=yellow>{calculator.GetAverageDuration()}</color></b>.";		
	}

}//SessionInformationUI class end.
