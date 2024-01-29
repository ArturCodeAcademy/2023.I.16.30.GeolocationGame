using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RecordTracker : MonoBehaviour
{
	public event Action RecordUpdated;

	[SerializeField] private PlayerController _playerController;

	public List<Record> Records { get; private set; }
	public Record CurrentRecord { get; private set; }

	private const string RECORDS_FILE = "records";
	private const int RECORDS_COUNT = 10;

	private void Awake()
	{
		CurrentRecord = new Record();
		string json = Resources.Load<TextAsset>(RECORDS_FILE).text;
		Records = JsonConvert.DeserializeObject<List<Record>>(json);

		Records ??= new () { CurrentRecord };
	}

	private void OnEnable()
	{
		_playerController.OnLand += OnLand;
	}

	private void OnDisable()
	{
		_playerController.OnLand -= OnLand;
	}

	private void OnLand(Collision2D collision)
	{
		if (!collision.transform.TryGetComponent(out LevelColumn _))
			return;

		float distance = collision.transform.position.x;
		if (distance > CurrentRecord.Distance)
		{
			CurrentRecord.Distance = distance;
			CurrentRecord.Time = Time.time;
			RecordUpdated?.Invoke();
		}
	}

	private void OnDestroy()
	{
		if (Records.Count < RECORDS_COUNT)
		{
			Records.Add(CurrentRecord);
		}
		else if (CurrentRecord.Distance > Records[^1].Distance)
		{
			Records[^1] = CurrentRecord;
		}

		Records.Sort((a, b) => b.Distance.CompareTo(a.Distance));
		
		string json = JsonConvert.SerializeObject(Records);
		File.WriteAllText(Application.dataPath + "/Resources/" + RECORDS_FILE + ".json", json);
	}
}
