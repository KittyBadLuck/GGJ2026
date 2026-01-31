using UnityEngine;
using MyBox;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [Scene]
    public string puzzleScene;

    public void StartLevel()
    {
        SceneManager.LoadScene(puzzleScene);
    }
}
