using DG.Tweening;
using MyBox;
using UnityEngine;

public class AlarmWarning : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer spriteVfx;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            TriggerAlarmWarning();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            StopAlarmWarning();
        }
    }

    [ButtonMethod]
    private void TriggerAlarmWarning()
    {
        spriteRenderer.DOFade(0, 0.1f).SetLoops(-1, LoopType.Yoyo);
        spriteVfx.SetAlpha(1);
    }
    private void StopAlarmWarning()
    {
        spriteRenderer.DOKill();
        spriteRenderer.DOFade(1, 0.1f);
        spriteVfx.SetAlpha(0);
    }
}
