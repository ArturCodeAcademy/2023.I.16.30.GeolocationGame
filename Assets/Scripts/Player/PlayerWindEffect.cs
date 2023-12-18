using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerController))]
public class PlayerWindEffect : MonoBehaviour
{
	public MeteoData? CurrentMeteoData { get; private set; }
	public event Action<MeteoData?> OnMeteoDataChanged;

	[SerializeField, Range(0.01f, 500)] private float _windSpeedDivider = 10;

	private bool _isFlying;

	private Rigidbody2D _rb;
	private PlayerController _playerController;

	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
		_playerController = GetComponent<PlayerController>();
	}

	private void FixedUpdate()
	{
		if (!_isFlying || CurrentMeteoData is null)
			return;

		Vector2 direction = (Vector2)(Quaternion.Euler(0, 0, CurrentMeteoData.WindDirection) * Vector2.up);
		_rb.AddForce(direction * CurrentMeteoData.WindSpeed / _windSpeedDivider, ForceMode2D.Force);
	}

	private void OnEnable()
	{
		_playerController.OnRelease += OnRelease;
		_playerController.OnLand += OnLand;
	}

	private void OnDisable()
	{
		_playerController.OnRelease -= OnRelease;
		_playerController.OnLand -= OnLand;
	}

	private void OnLand(Collision2D other)
	{
		_isFlying = false;
		CurrentMeteoData = other.gameObject.GetComponent<LevelColumn>()?.Data;
		OnMeteoDataChanged?.Invoke(CurrentMeteoData);
	}

	private void OnRelease()
	{
		_isFlying = true;
	}
}