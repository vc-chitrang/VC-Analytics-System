using UnityEngine;

public class DeviceAndTechnicalInfoUI:DataUIBase {
	[SerializeField] private ReusableScrollPanel reusableScrollPanel;
	public override void Init(AnalyticsEvent data) {
		base.Init(data);
		reusableScrollPanel.Display(data.Parameters);
	}
}//DeviceAndTechnicalInfoUI class end
