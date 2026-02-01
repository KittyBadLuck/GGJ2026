using UnityEngine;

public class SFX_Manager : MonoBehaviour
{
    public AudioSource successSound;
    public AudioSource failureSound;
    public AudioSource freezeSound;
    public AudioSource pickupSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PuzzleManager.OnWin += PlayWinSound;
        Alarm.OnAlarmTriggered += PlayFailSound;
        DragController.OnItemFroze += PlayFreezeSound;
        DragController.OnItemPickup += PlayPickupSound;
    }

    private void OnDestroy()
    {
        PuzzleManager.OnWin -= PlayWinSound;
        Alarm.OnAlarmTriggered -= PlayFailSound;
        DragController.OnItemFroze -= PlayFreezeSound;
        DragController.OnItemPickup -= PlayPickupSound;
    }

    private void PlayWinSound(float percentage)
    {
        successSound.Play();
    }
    private void PlayFailSound()
    {
        failureSound.Play();
    }
    private void PlayFreezeSound()
    {
        freezeSound.Play();
    }

    private void PlayPickupSound()
    {
        pickupSound.Play();
    }
}
