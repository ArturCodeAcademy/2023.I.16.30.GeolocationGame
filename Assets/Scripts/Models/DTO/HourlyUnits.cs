using Newtonsoft.Json;

public class HourlyUnits
{
	[JsonProperty("time")]
	public string Time { get; set; }
	[JsonProperty("temperature_2m")]
	public string Temperature { get; set; }
	[JsonProperty("windspeed_10m")]
	public string WindSpeed { get; set; }
	[JsonProperty("winddirection_10m")]
	public string WindDirection { get; set; }
}