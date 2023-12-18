using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindInfoPanel : MonoBehaviour
{
	[SerializeField] private PlayerWindEffect _playerWindEffect;

	[Space(5)]
	[SerializeField] private Image _windDirectionImage;
	[SerializeField] private TMP_Text _windSpeedText;

	[Space(5)]
	[SerializeField, Min(1)] private float _windDirectionImageRotationSpeed = 100;
	[SerializeField, Min(1)] private float _windSpeedLerpSpeed = 1;

	private float _windDirection = 0;
	private float _windSpeed = 0;
	private MeteoData _meteoData;

	private void OnEnable()
	{
		_playerWindEffect.OnMeteoDataChanged += OnMeteoDataChanged;
	}

	private void OnDisable()
	{
		_playerWindEffect.OnMeteoDataChanged -= OnMeteoDataChanged;
	}

	private void Update()
	{
		float targetDirection = _meteoData?.WindDirection ?? 0;
		float targetSpeed = _meteoData?.WindSpeed ?? 0;

		if (targetDirection == _windDirection && targetSpeed == _windSpeed)
			return;

		_windSpeed = Mathf.Lerp(_windSpeed,
								targetSpeed,
								_windSpeedLerpSpeed * Time.deltaTime);
		_windSpeedText.text = $"{_windSpeed:0.0}\nm / s";

		_windDirection = Mathf.LerpAngle(_windDirection,
								targetDirection,
								_windDirectionImageRotationSpeed * Time.deltaTime);
		_windDirectionImage.transform.rotation = Quaternion.Euler(0, 0, _windDirection);
	}

	private void OnMeteoDataChanged(MeteoData? meteoData)
	{
		_meteoData = meteoData;
	}
}