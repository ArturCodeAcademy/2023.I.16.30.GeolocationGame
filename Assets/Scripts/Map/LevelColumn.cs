using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class LevelColumn : MonoBehaviour
{
	public MeteoData Data { get; private set; }

	[SerializeField] private Transform _infoPanel;
	[SerializeField] private TMP_Text _dateText;
	[SerializeField] private TMP_Text _temperatureText;
	[SerializeField] private TMP_Text _windText;
	[SerializeField] private Transform _windDirection;
	[SerializeField] Vector2 _infoPanelOffset;

	private BoxCollider2D _collider;
	private SpriteRenderer _renderer;

	private void Awake()
	{
		_collider = GetComponent<BoxCollider2D>();
		_renderer = GetComponent<SpriteRenderer>();
	}

	public void SetHeight(float height)
	{
		_collider.size = new Vector2(_collider.size.x, height);
		_renderer.size = new Vector2(_renderer.size.x, height);
		transform.position = new Vector3(transform.position.x,
											height / 2,
											transform.position.z);

		_infoPanel.transform.localPosition = new Vector3(0, height / 2)
										+ (Vector3)_infoPanelOffset;
	}

	public void SetInfo(MeteoData data)
	{
		Data = data;
		_temperatureText.text = $"{data.Temperature}°";
		_windText.text = $"{data.WindSpeed} m/s";
		_windDirection.rotation = Quaternion.Euler(0, 0, data.WindDirection);
		_dateText.text = $"{data.Time:dd.mm.yyyy}\n{data.Time:HH:mm}";
	}

	public void SetColor(Color color)
	{
		_renderer.color = color;
	}
}