using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class VCButton : Button {
	private UserInteractionTracker _tracker;
	protected override void Awake() {
		_tracker = new UserInteractionTracker(Constant.BUTTON_CLICK_CONST);
		onClick.AddListener(() => {
			string parameterKey = $"{this.gameObject.name}";
			object data = AnalyticsManager.Instance.GetParameterData(_tracker.EventName, parameterKey);
			int intValue = Convert.ToInt32((data is null) ? 0 : data);			
			AnalyticsEvent _eventData = _tracker.Create(new Dictionary<string,object>(){
				{parameterKey, intValue + 1}
			});

			AnalyticsManager.Instance.AddOrUpdateParams(_eventData, parameterKey);
			AnalyticsManager.Instance.StoreData();
		});
	}
}
