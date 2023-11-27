using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(DataRequester))]
public class LevelGenerator : MonoBehaviour
{
    public List<LevelColumn> Columns { get; private set; } = new List<LevelColumn>();

    [SerializeField] private LevelColumn _columnPrefab;

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
	}
}
