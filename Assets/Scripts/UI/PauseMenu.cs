using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject overlay;
    private bool _isOpen = false;

    private void Update()
    {
        if (!EndScreen.isOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isOpen)
            {
                Resume();
            }
            else
            {
                Open();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        _isOpen = false;
        overlay.SetActive(false);
    }
    public void Open()
    {
        Time.timeScale = 0;
        _isOpen = true;
        overlay.SetActive(true);
    }

    public void Retry()
    {
        Scene s = SceneManager.GetActiveScene();
        SceneManager.LoadScene(s.buildIndex);
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene(0);
    }
}
