using MyBox;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public ShapeDiffDetector shapeDiffDetector;
    public float winThreshold = 0.05f;
    public Inventory inventory;

    [ButtonMethod]
    public bool CheckWin()
    {
        if(shapeDiffDetector.DetectDiffPercentage() < winThreshold)
        {
            OnWin();
            return true;
        }
        else
        {
            if(inventory.GetItemCount() == 0)
            {
                OnFail();
            }
            return false;
        }
    }

    private void OnWin()
    {
        Debug.Log("Puzzle Solved!");
    }
    private void OnFail()
    {
        Debug.Log("Puzzle Failed :(!");
    }

}
