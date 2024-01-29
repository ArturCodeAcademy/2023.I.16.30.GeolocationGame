using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RecordsUI : MonoBehaviour
{
	[SerializeField] private TMP_Text _prefabText;
	[SerializeField] private Transform _content;

	[Space(5)]
    [SerializeField] private RecordTracker _recordTracker;

	private List<TMP_Text> _texts = new();

	private void Start()
	{
		for (int i = 0; i < _recordTracker.Records.Count + 1; i++)
		{
			TMP_Text text = Instantiate(_prefabText, _content);
			_texts.Add(text);
		}

		_prefabText.gameObject.SetActive(false);
		OnRecordUpdated();
	}

	private void OnEnable()
    {
		_recordTracker.RecordUpdated += OnRecordUpdated;
	}

    private void OnDisable()
    {
		_recordTracker.RecordUpdated -= OnRecordUpdated;
	}

	private void OnRecordUpdated()
	{
		List<Record> records = _recordTracker.Records
											.Append(_recordTracker.CurrentRecord)
											.OrderByDescending(r => r.Distance)
											.ToList();

		for (int i = 0; i < records.Count; i++)
		{
			int minutes = (int)(records[i].Time / 60);
			float seconds = records[i].Time % 60;
			_texts[i].text = $"{i + 1}. {records[i].Distance:F2}m\t{minutes:00}:{seconds:00.000}";

			if (records[i] == _recordTracker.CurrentRecord)
				_texts[i].color = Color.green;
			else
				_texts[i].color = Color.white;
		}
	}
}
