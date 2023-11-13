using Newtonsoft.Json;

public class HourlyData
{
    [JsonProperty("time")]
    public string[] Time { get; set; }
    [JsonProperty("temperature_2m")]
    public float[] Temperature { get; set; }
    [JsonProperty("windspeed_10m")]
    public float[] WindSpeed { get; set; }
    [JsonProperty("winddirection_10m")]
    public int[] WindDirection { get; set; }
}
