using System;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    public static event Action OnAlarmTriggered;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {

            OnAlarmTriggered?.Invoke();
        }
    }
}
