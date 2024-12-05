using UnityEngine;

public class ButtonClickUI : DataUIBase
{
   [SerializeField] private ReusableScrollPanel reusableScrollPanel;

   public override void Init(AnalyticsEvent data) {
		base.Init(data);

		foreach (var i in data.Parameters) {
			reusableScrollPanel.Display($"<b>{i.Key}</b> clicked <b>{i.Value}</b> times.");
		}
	}
}
