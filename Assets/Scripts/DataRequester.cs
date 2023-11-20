using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class DataRequester : MonoBehaviour
{
	public List<MeteoData> MeteoDataList { get; private set; } = new List<MeteoData>();
    public bool IsDataReady { get; private set; } = false;

    private const string BASE_URL = "https://api.open-meteo.com/v1/forecast?";
	private HttpClient _client = new HttpClient();

	private async void Start()
	{
		await GetResponseAsync(Container.SelectedCoords);
	}

	private async Task GetResponseAsync(LocationCoords coords)
	{
		string url = $"{BASE_URL}" +
						$"latitude={coords.Latitude.ToString().Replace(',', '.')}" +
						$"&longitude={coords.Longitude.ToString().Replace(',', '.')}" +
						$"&hourly=temperature_2m" +
						$"&hourly=windspeed_10m" +
						$"&hourly=winddirection_10m";

		string response = await _client.GetStringAsync(url);
		
		var data = JsonConvert.DeserializeObject<LocationData>(response);

		for (int i = 0; i < data.Hourly.Time.Length; i++)
		{
			var mData = new MeteoData();
			if (DateTime.TryParse(data.Hourly.Time[i], out DateTime time))
				mData.Time = time;

			mData.Temperature = data.Hourly.Temperature[i];
			mData.WindSpeed = data.Hourly.WindSpeed[i];
			mData.WindDirection = data.Hourly.WindDirection[i];
		}

		IsDataReady = true;
	}
}
