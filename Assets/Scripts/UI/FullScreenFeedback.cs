using DG.Tweening;
using MyBox;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenFeedback : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private float m_MaxIntensity = 6f;
    [SerializeField]
    private float m_FlashInDuration = 0.1f;
    [SerializeField]
    private float m_FlasOuthDuration = 0.2f;
    [SerializeField]
    private float m_FlashPause = 0.1f;
    private Material m_MaterialInstance;
    private Image m_Image;

    [Header("CameraShake")]
    public float duratiom;
    public float strenght;
    public int vibrato;
    public int randomness;

    #endregion

    #region Methods
    private void Start()
    {
        m_Image = GetComponent<Image>();
        m_MaterialInstance = Instantiate(m_Image.material);

        m_Image.material = m_MaterialInstance;

        Alarm.OnAlarmTriggered += PlayAlarmFeedback;
    }
    private void OnDestroy()
    {
        Alarm.OnAlarmTriggered -= PlayAlarmFeedback;
    }
    [ButtonMethod]
    public void PlayAlarmFeedback()
    {
        StartCoroutine(FeedbackRoutine());
        TriggerAlarmWarning();
    }

    private void TriggerAlarmWarning()
    {
        var go = Camera.main.gameObject;

        go.transform.DOShakeRotation(duratiom, new Vector3(0, 0, strenght), vibrato, randomness, false);
    }
    private IEnumerator FeedbackRoutine()
    {
        yield return m_MaterialInstance.DOFloat(m_MaxIntensity, "_VignetteIntensity", m_FlashInDuration).SetEase(Ease.OutQuad).WaitForCompletion();
        yield return new WaitForSeconds(m_FlashPause);
        yield return m_MaterialInstance.DOFloat(0, "_VignetteIntensity", m_FlasOuthDuration).SetEase(Ease.OutQuad).WaitForCompletion();
    }


    #endregion
}
