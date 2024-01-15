using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
	[SerializeField] private PlayerController _playerController;

    private const int MAIN_MENU_SCENE_INDEX = 0;

	private void Start()
	{
		_playerController.OnFallOutOfLevel += Show;
		gameObject.SetActive(false);
	}

	private void OnDestroy()
	{
		_playerController.OnFallOutOfLevel -= Show;
	}

	private void Show()
	{
		gameObject.SetActive(true);
	}

	public void LoadMainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE_INDEX);
    }

    public void Restart()
    {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
