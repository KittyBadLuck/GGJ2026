using MyBox;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : Singleton<PuzzleManager>
{
    [Range(0, 1)]
    public float WIN_THRESHOLD = 0.05f;
    
    

    public ShapeDiffDetector shapeDiffDetector;

    public Inventory inventory;

    public static event Action<float> OnWin;
    public static event Action<float> OnFail;
    public static event Action OnDisplay;
    private void Start()
    {
        DragController.OnItemFroze += MarkForCheck;
        Alarm.OnAlarmTriggered += AlarmTriggered;
    }
    private void OnDestroy()
    {
        DragController.OnItemFroze -= MarkForCheck;
        Alarm.OnAlarmTriggered -= AlarmTriggered;
    }

    public void DisplayVisuals()
    {
        OnDisplay?.Invoke();
    }

    private void AlarmTriggered()
    {
        float diff = shapeDiffDetector.DetectDiffPercentage();
        SaveScore(1 - diff);
        OnFail?.Invoke(1-diff);

    }
    private void MarkForCheck()
    {
        StartCoroutine(WaitForEndOfFrame());
    }
    [ButtonMethod]
    public void CheckWin()
    {
        float diff = shapeDiffDetector.DetectDiffPercentage();
        if (diff < WIN_THRESHOLD)
        {
            SaveScore(1 - diff);
            OnWin?.Invoke(1-diff);
        }
        else
        {
            if(inventory.GetItemCount() == 0)
            {
                SaveScore(1 - diff);
                OnFail?.Invoke(1 - diff);
            }
        }
    }
    private IEnumerator WaitForEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        CheckWin();
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
