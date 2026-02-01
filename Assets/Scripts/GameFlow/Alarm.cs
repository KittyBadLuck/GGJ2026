using DG.Tweening;
using MyBox;
using System;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    public static event Action OnAlarmTriggered;
    public SpriteRenderer sprite;
    public SpriteRenderer collideSprite;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            collideSprite.SetAlpha(1);
            OnAlarmTriggered?.Invoke();
        }
    }

    public void FadeIn()
    {
        sprite.DOFade(1, 0.2f);
    }
}
