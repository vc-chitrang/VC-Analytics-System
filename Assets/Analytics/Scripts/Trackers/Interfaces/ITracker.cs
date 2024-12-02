public interface ITracker {
	public string EventName {
		get;
	}

	public abstract AnalyticsEvent Create();
}