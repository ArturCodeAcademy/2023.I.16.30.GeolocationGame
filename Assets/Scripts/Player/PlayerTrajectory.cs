using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PlayerTrajectory : MonoBehaviour
{
	[SerializeField, Min(3)] private int _pointsCount;
	[SerializeField, Range(0.1f, 1)] private float _deltaX;

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
		_lineRenderer.positionCount = _pointsCount;
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
		float force = _playerController.CalculateForce();
		Vector2 direction = _playerController.GetDirection();
		CalculateTrajectoryPoints(force, direction);
	}

	private void CalculateTrajectoryPoints(float force, Vector2 direction)
	{
		float timeStep = _deltaX / force;
		float currentTime = 0f;

		for (int i = 0; i < _pointsCount; i++)
		{
			Vector3 pointPosition = CalculatePointPosition(currentTime, force, direction);
			_lineRenderer.SetPosition(i, pointPosition);

			currentTime += timeStep;
		}
	}

	private Vector3 CalculatePointPosition(float time, float force, Vector2 direction)
	{
		float x = time * force * direction.x;
		float y = time * force * direction.y - 0.5f * Mathf.Abs(Physics2D.gravity.y) * time * time;

		return new Vector3(x, y, 0f);
	}
}
