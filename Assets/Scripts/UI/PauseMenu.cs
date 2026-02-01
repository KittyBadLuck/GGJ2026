using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject overlay;
    private bool _isOpen = false;

    public Sprite musicMutedSprite;
    public Sprite musicUnmutedSprite;
    public Image musicImage;

    public Sprite soundMutedSprite;
    public Sprite soundUnmutedSprite;
    public Image soundImage;

    private void Start()
    {
        musicImage.sprite = !AudioManager.Instance.isMusicMuted ? musicUnmutedSprite : musicMutedSprite;
        soundImage.sprite = !AudioManager.Instance.isSFXMuted ? soundUnmutedSprite : soundMutedSprite;
    }
    private void Update()
    {
        if (!EndScreen.isOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            TryOpen();
        }
    }
    public void SwitchMusic()
    {
        AudioManager.Instance.MuteMusic();
        musicImage.sprite = !AudioManager.Instance.isMusicMuted ? musicUnmutedSprite : musicMutedSprite;
    }
    public void SwitchSFX()
    {
        AudioManager.Instance.MuteSFX();
        soundImage.sprite = !AudioManager.Instance.isSFXMuted ? soundUnmutedSprite : soundMutedSprite;
    }
    public void TryOpen()
    {
        if (EndScreen.isOpen)
        {
            return;
        }
        if (_isOpen)
        {
            Resume();
        }
        else
        {
            Open();
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
        Time.timeScale = 1;
        Scene s = SceneManager.GetActiveScene();
        SceneManager.LoadScene(s.buildIndex);
    }

    public void ReturnToMain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
