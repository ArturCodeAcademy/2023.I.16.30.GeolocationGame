using UnityEngine;

public class WindEffectParticle : MonoBehaviour
{
	[SerializeField] private PlayerWindEffect _playerWindEffect;

	private ParticleSystem _particleSystem;

	private void Awake()
	{
		_particleSystem = GetComponent<ParticleSystem>();
	}

	private void OnEnable()
	{
		_playerWindEffect.OnMeteoDataChanged += OnMeteoDataChanged;
	}

	private void OnDisable()
	{
		_playerWindEffect.OnMeteoDataChanged -= OnMeteoDataChanged;
	}

	private void OnMeteoDataChanged(MeteoData? meteoData)
	{
		transform.rotation = Quaternion.Euler(0, 0, meteoData.WindDirection);
		_particleSystem.startSpeed = meteoData.WindSpeed;
	}
}