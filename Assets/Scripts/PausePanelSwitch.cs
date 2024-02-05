using UnityEngine;

public class PausePanelSwitch : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _pausePanel;

    private bool _isPaused = false;

    private void OnEnable()
    {
		_playerController.OnFallOutOfLevel += OnFallOutOfLevel;
	}

    private void OnDisable()
    {
        _playerController.OnFallOutOfLevel -= OnFallOutOfLevel;
    }

    private void OnFallOutOfLevel()
    {
		enabled = false;
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			_isPaused = !_isPaused;
            _pausePanel.SetActive(_isPaused);
		}
    }
}
