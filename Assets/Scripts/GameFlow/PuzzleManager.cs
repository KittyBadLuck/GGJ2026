using MyBox;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    public const float WIN_THRESHOLD = 0.05f;

    public ShapeDiffDetector shapeDiffDetector;

    public Inventory inventory;

    public static event Action<float> OnWin;
    public static event Action<float> OnFail;

    [ButtonMethod]
    public bool CheckWin()
    {
        float diff = shapeDiffDetector.DetectDiffPercentage();
        if (diff < WIN_THRESHOLD)
        {
            SaveScore(1 - diff);
            OnWin?.Invoke(1-diff);
            return true;
        }
        else
        {
            if(inventory.GetItemCount() == 0)
            {
                SaveScore(1 - diff);
                OnFail?.Invoke(1 - diff);
            }
            return false;
        }
    }

    private void SaveScore(float score)
    {
        Scene s = SceneManager.GetActiveScene();
        var precentScore = PlayerPrefs.GetFloat(s.name, 0);

        if(score * 100 > precentScore)
        {
            PlayerPrefs.SetFloat(s.name, score * 100);
        }

    }

}
