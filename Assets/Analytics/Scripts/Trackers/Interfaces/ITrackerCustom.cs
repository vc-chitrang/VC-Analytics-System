public interface ITrackerCustom {
	public string EventName {
		get;
	}

	public abstract AnalyticsEvent Create();
}
