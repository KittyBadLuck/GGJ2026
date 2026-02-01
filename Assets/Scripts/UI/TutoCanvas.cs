using DG.Tweening;
using UnityEngine;

public class TutoCanvas : MonoBehaviour
{
    public CanvasGroup CanvasGroup;

    private void Start()
    {
        PuzzleManager.OnDisplay += Display;
    }
    private void OnDestroy()
    {
        PuzzleManager.OnDisplay -= Display;
    }

    private void Display()
    {

        CanvasGroup.DOFade(1, 1f);
    }
}
