using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class DataRequester : MonoBehaviour
{
    public bool IsDataReady { get; private set; } = false;

    private const string BASE_URL = "https://api.open-meteo.com/v1/forecast?";
	private HttpClient _client = new HttpClient();

	private async void Start()
	{
		await GetResponseAsync(Container.SelectedCoords);
	}

	private async Task GetResponseAsync(LocationCoords coords)
	{
		string url =	$"{BASE_URL}" +
						$"latitude={coords.Latitude.ToString().Replace(',', '.')}" +
						$"&longitude={coords.Longitude.ToString().Replace(',', '.')}" +
						$"&hourly=temperature_2m" +
						$"&hourly=windspeed_10m";

		string response = await _client.GetStringAsync(url);
		Debug.Log(response);
	}
}
