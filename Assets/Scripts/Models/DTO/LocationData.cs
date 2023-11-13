using Newtonsoft.Json;

public class LocationData
{
	[JsonProperty("latitude")]
	public double Latitude { get; set; }
	[JsonProperty("longitude")]
	public double Longitude { get; set; }
	[JsonProperty("generationtime_ms")]
	public double GenerationTimeMs { get; set; }
	[JsonProperty("utc_offset_seconds")]
	public int UtcOffsetSeconds { get; set; }
	[JsonProperty("timezone")]
	public string Timezone { get; set; }
	[JsonProperty("timezone_abbreviation")]
	public string TimezoneAbbreviation { get; set; }
	[JsonProperty("elevation")]
	public double Elevation { get; set; }
	[JsonProperty("hourly_units")]
	public HourlyUnits HourlyUnits { get; set; }
	[JsonProperty("hourly")]
	public HourlyData Hourly { get; set; }
}
