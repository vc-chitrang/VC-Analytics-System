public interface ITracker {
	public string EventName {
		get;
	}

	public AnalyticsEvent Create();
}