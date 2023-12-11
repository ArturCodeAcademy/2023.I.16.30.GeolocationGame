using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PlayerForceLine : MonoBehaviour
{
	[SerializeField] private Vector3 _offset;

	private LineRenderer _lineRenderer;
	private PlayerController _playerController;

	private void Awake()
	{
		_playerController = GetComponentInParent<PlayerController>();
		_lineRenderer = GetComponent<LineRenderer>();
	}

	private void Start()
	{
		HideLine();
		_lineRenderer.positionCount = 2;
	}

	private void OnEnable()
	{
		_playerController.OnStartDrag += ShowLine;
		_playerController.OnRelease += HideLine;
		_playerController.OnDrag += UpdateLine;
	}

	private void OnDisable()
	{
		_playerController.OnStartDrag -= ShowLine;
		_playerController.OnRelease -= HideLine;
		_playerController.OnDrag -= UpdateLine;
	}

	private void ShowLine()
	{
		_lineRenderer.enabled = true;
	}

	private void HideLine()
	{
		_lineRenderer.enabled = false;
	}

	private void UpdateLine(float _)
	{
		_lineRenderer.SetPosition(1, _playerController.transform.position + _offset);
		Vector2 direction = _playerController.GetDirection();
		float distance = Vector2.Distance(_playerController.transform.position,
									Camera.main.ScreenToWorldPoint(Input.mousePosition));
		distance = Mathf.Clamp(distance, 0, _playerController.MaxDistance);
		_lineRenderer.SetPosition(0, _playerController.transform.position - (Vector3)direction * distance + _offset);
	}
}