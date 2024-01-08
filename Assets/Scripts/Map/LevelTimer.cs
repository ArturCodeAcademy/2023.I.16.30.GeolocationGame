using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
	[SerializeField] private PlayerController _playerController;
	[SerializeField] private DataRequester _dataRequester;
	[SerializeField] private TMP_Text _timerText;

	private float _time;
	private bool _isPlaying = true;

	private void Start()
	{
		_playerController.OnFallOutOfLevel += Stop;
	}

	private void OnDestroy()
	{
		_playerController.OnFallOutOfLevel -= Stop;
	}

	private void Stop()
	{
		_isPlaying = false;
	}

	private void Update()
	{
		if (_dataRequester.IsDataReady && _isPlaying)
		{
			_time += Time.deltaTime;
			int m = (int)_time / 60;
			_timerText.text = $"{m:00}:{_time - m * 60:00.000}";
		}
	}
}