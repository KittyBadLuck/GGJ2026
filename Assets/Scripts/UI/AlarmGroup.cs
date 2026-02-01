using System.Collections;
using UnityEngine;

public class AlarmGroup : MonoBehaviour
{
    private void Start()
    {
        PuzzleManager.OnDisplay += DisplayAll;
    }

    private void OnDestroy()
    {
        PuzzleManager.OnDisplay -= DisplayAll;
    }

    private void DisplayAll()
    {
        StartCoroutine(Display());
    }

    IEnumerator Display()
    {
        var alarms = FindObjectsByType<Alarm>(FindObjectsSortMode.None);

        foreach (var alarm in alarms)
        {
            alarm.FadeIn();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
