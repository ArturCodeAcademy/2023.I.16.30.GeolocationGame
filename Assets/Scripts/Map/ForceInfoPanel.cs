using UnityEngine;
using UnityEngine.UI;

public class ForceInfoPanel : MonoBehaviour
{
	[SerializeField] private PlayerController _playerController;

	[Space(5)]
	[SerializeField] private Image _forceForegroundImage;
	[SerializeField] private Outline _forceOutline;
	[SerializeField] private Gradient _forceGradient;

	private void Start()
	{
		HideImage();
	}

	private void OnEnable()
	{
		_playerController.OnDrag += OnForceChanged;
		_playerController.OnRelease += HideImage;
	}

	private void OnDisable()
	{
		_playerController.OnDrag -= OnForceChanged;
		_playerController.OnRelease -= HideImage;
	}

	private void OnForceChanged(float force)
	{
		_forceForegroundImage.fillAmount = force;
		Color color = _forceGradient.Evaluate(force);
		_forceForegroundImage.color = color;
		_forceOutline.effectColor = color;
	}

	private void HideImage()
	{
		_forceForegroundImage.fillAmount = 0;
		_forceOutline.effectColor = Color.white;
	}
}