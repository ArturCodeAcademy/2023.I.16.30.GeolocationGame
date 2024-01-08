using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(DataRequester))]
public class LevelGenerator : MonoBehaviour
{
    public List<LevelColumn> Columns { get; private set; } = new List<LevelColumn>();

    [SerializeField] private LevelColumn _columnPrefab;
	[SerializeField] private GameObject _ground;
	[SerializeField] private GameObject _stop;
	[SerializeField] private SpriteRenderer _bg;

	[Space(5)]
    [SerializeField, Min(1)] private float _minInterval;
    [SerializeField, Min(1)] private float _maxInterval;
    [SerializeField, Min(1)] private float _minHeight;
    [SerializeField, Min(1)] private float _maxHeight;
    [SerializeField] private Gradient _temperatureGradient;

    [Space(5)]
    [SerializeField] private Transform _player;

    private DataRequester _dataRequester;

    private void Awake()
    {
		_dataRequester = GetComponent<DataRequester>();
	}

    private IEnumerator Start()
    {
		WaitUntil wait = new (() => _dataRequester.IsDataReady);
		yield return wait;

		GenerateLevel();
	}

	private void GenerateLevel()
	{
		float minT = _dataRequester.MeteoDataList.Min(m => m.Temperature);
		float maxT = _dataRequester.MeteoDataList.Max(m => m.Temperature);
        float deltaT = maxT - minT;
        float x = 0;

        foreach (var data in _dataRequester.MeteoDataList)
        {
			var column = Instantiate(_columnPrefab, transform);
            column.transform.position = new Vector3(x, 0, 0);
            x += Random.Range(_minInterval, _maxInterval);
            float temperaturePercent = (data.Temperature - minT) / deltaT;
            float height = Mathf.Lerp(_minHeight, _maxHeight, temperaturePercent);
            column.SetHeight(height);
            column.SetColor(_temperatureGradient.Evaluate(temperaturePercent));
            column.SetInfo(data);

            Columns.Add(column);
		}

        float posX = Columns[0].transform.position.x;
        float posY = Columns[0].GetComponent<SpriteRenderer>().size.y;
        _player.position = new Vector3(posX, posY, 0);

		x = Columns[^1].transform.position.x + 1;
		_ground.transform.localScale = new Vector3(x, 1, 1);
        _ground.transform.position = new Vector3(x / 2 - 0.5f, _ground.transform.position.y);
		_stop.transform.position = new Vector3(x, 0);
		_bg.size = new Vector2(x, _bg.size.y);
        _bg.transform.position = new Vector3(x / 2 - 0.5f, _bg.transform.position.y);
	}
}
