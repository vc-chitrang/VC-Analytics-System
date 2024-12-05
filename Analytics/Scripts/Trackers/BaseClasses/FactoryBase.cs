using System.Collections.Generic;

public abstract class FactoryBase {
	public abstract AnalyticsEvent CreateTracker(string eventName, Dictionary<string, object> eventParams);
	public string GetLog(ITracker product) {
		return "";
	}
}
