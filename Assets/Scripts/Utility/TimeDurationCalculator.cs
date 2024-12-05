using System;

public class TimeDurationCalculator {
	private TimeSpan totalDuration = TimeSpan.Zero;
	private int count = 0;

	// Adds a new time duration string
	public void AddDuration(string timeString) {
		if (string.IsNullOrEmpty(timeString))
			return; // Skip invalid durations

		DateTime time = DateTime.ParseExact(timeString,"dd-MM-yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture);
		TimeSpan duration = time.TimeOfDay;
		totalDuration += duration;
		count++;
	}

	// Returns the total duration in "HH:mm:ss" format
	public string GetTotalDuration() {
		return $"{totalDuration.Hours:D2}H:{totalDuration.Minutes:D2}m:{totalDuration.Seconds:D2}s";
	}

	// Returns the average duration in "HH:mm:ss" format
	public string GetAverageDuration() {
		if (count == 0)
			return "00H:00m:00s"; // Handle division by zero
		TimeSpan averageDuration = new TimeSpan(totalDuration.Ticks / count);
		return $"{averageDuration.Hours:D2}H:{averageDuration.Minutes:D2}m:{averageDuration.Seconds:D2}s";
	}
}
