using DG.Tweening;
using MyBox;
using UnityEngine;

public class AlarmWarning : MonoBehaviour
{
    public float duratiom;
    public float strnght;
    public int vibrato;
    public int randomness;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            TriggerAlarmWarning();
        }
    }

    [ButtonMethod]
    private void TriggerAlarmWarning()
    {
        var go = Camera.main.gameObject;

        go.transform.DOShakeRotation(duratiom, new Vector3(0,0, strnght), vibrato, randomness, false);
    }
}
