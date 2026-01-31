using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public GameObject overlay;
    public static bool isOpen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI resultText;
    public GameObject playNextLevelButton;

    private void Start()
    {
        isOpen = false;
        PuzzleManager.OnWin += OpenWin;
        PuzzleManager.OnFail += OpenFail;
    }
    private void OnDestroy()
    {
        PuzzleManager.OnWin -= OpenWin;
        PuzzleManager.OnFail -= OpenFail;
    }
    private void OpenWin(float percentage)
    {
        Open(true, percentage);
    }
    private void OpenFail(float percentage)
    {
        Open(false, percentage);
    }
    private void Open(bool isWin, float percentage)
    {
        isOpen = true;
        overlay.SetActive(true);
        scoreText.text = $"Score : {percentage * 100}%";
        resultText.text = isWin ? "WIN" : "LOOSE";

        Scene s = SceneManager.GetActiveScene();
        int sCount = SceneManager.sceneCountInBuildSettings;

        if (isWin && s.buildIndex + 1 < sCount )
        {
            playNextLevelButton.SetActive(true);
        }
        else
        {
            playNextLevelButton.SetActive(false);
        }
    }
    public void RetryLevel()
    {
        Scene s = SceneManager.GetActiveScene();
        SceneManager.LoadScene(s.buildIndex);
    }
    public void PlayNextLevel()
    {
        Scene s = SceneManager.GetActiveScene();
        int sCount = SceneManager.sceneCountInBuildSettings;

        if (s.buildIndex + 1 < sCount)
        {
            SceneManager.LoadScene(s.buildIndex+1);
        }
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
