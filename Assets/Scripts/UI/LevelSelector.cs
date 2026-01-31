using UnityEngine;
using MyBox;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [Scene]
    public string puzzleScene;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI levelNameText;
    public Button levelButton;

    public void SetUp(bool unlocked, string scene, int index)
    {
        puzzleScene = scene;
        if (PlayerPrefs.HasKey(puzzleScene))
        {
            bestScoreText.text = $"Best Score : {PlayerPrefs.GetFloat(puzzleScene)}%";
        }
        else
        {
            bestScoreText.text = $"Best Score : ??";
        }
        levelNameText.text = $"Level {index}";
        levelButton.interactable = unlocked;
    }
    public void StartLevel()
    {
        SceneManager.LoadScene(puzzleScene);
    }
}
