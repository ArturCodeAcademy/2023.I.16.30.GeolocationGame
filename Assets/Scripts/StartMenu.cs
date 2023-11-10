using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
	[SerializeField] private TMP_InputField _latitudeText;
	[SerializeField] private TMP_InputField _longitudeText;

	[Space(3)]
	[SerializeField] private GameObject _scrollViewContent;
	[SerializeField] private GameObject _prefabButton;

	private LocationCoords _coords = new LocationCoords();

	private void Awake()
	{
		LocationCoords[] cities = ReadCitiesFromResources();
		_prefabButton.SetActive(false);
		foreach (var city in cities)
		{
			var button = Instantiate(_prefabButton, _scrollViewContent.transform);
			button.SetActive(true);
			button.GetComponentInChildren<TMP_Text>().text = city.City;
			button.GetComponent<Button>().onClick.AddListener(() => OnCityButtonClicked(city));
		}
	}

	private LocationCoords[] ReadCitiesFromResources()
	{
		TextAsset citiesTextAsset = Resources.Load<TextAsset>("Cities");
		return Newtonsoft.Json.JsonConvert.DeserializeObject<LocationCoords[]>(citiesTextAsset.text)
			.GroupBy(c => c.City).Select(g => g.First()).OrderBy(c => c.City).ToArray();
	}

	private void OnCityButtonClicked(LocationCoords city)
	{
		_coords = city;
		_latitudeText.text = _coords.Latitude.ToString();
		_longitudeText.text = _coords.Longitude.ToString();
	}

	public void OnStartButtonClicked()
	{
		//Container.SelectedLocationCoords = _coords;
		SceneManager.LoadScene("Level");
	}

	public void OnExitButtonClicked()
	{
		Application.Quit();
	}

	public void OnLatitudeEndEdit(string value)
	{
		float.TryParse(value, out float result);
		_coords.Latitude = result;
	}

	public void OnLongitudeEndEdit(string value)
	{
		float.TryParse(value, out float result);
		_coords.Longitude = result;
	}
}
