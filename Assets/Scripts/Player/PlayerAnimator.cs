using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private PlayerController _playerController;

    private void Awake()
    {
		_animator = GetComponent<Animator>();
		_playerController = GetComponent<PlayerController>();
	}

    private void OnEnable()
    {
		_playerController.OnFallOutOfLevel += OnFallOutOfLevel;
		_playerController.OnRelease += OnJump;
		_playerController.OnLand += OnLand;
	}

	private void OnDisable()
	{
		_playerController.OnFallOutOfLevel -= OnFallOutOfLevel;
		_playerController.OnRelease -= OnJump;
		_playerController.OnLand -= OnLand;
	}

	private void OnFallOutOfLevel()
	{
		_animator.SetTrigger("Die");
	}

	private void OnJump()
	{
		_animator.SetTrigger("Jump");
	}

	private void OnLand(Collision2D _)
	{
		_animator.SetTrigger("Land");
	}
}
