using MyBox;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource mainMusic;
    public AudioMixer mainMixer;
    public bool isMusicMuted;
    public bool isSFXMuted;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

    }
    private void Start()
    {

        isMusicMuted = PlayerPrefs.GetInt("isMusicMuted", 0) == 1;
        isSFXMuted = PlayerPrefs.GetInt("isSFXMuted", 0) == 1;

        if (isMusicMuted)
        {
            mainMixer.SetFloat("VolumeMusic", -80);
        }
        else
        {
            mainMixer.SetFloat("VolumeMusic", 0);
        }

        if (isSFXMuted)
        {
            mainMixer.SetFloat("VolumeSFX", -80);
        }
        else
        {
            mainMixer.SetFloat("VolumeSFX", 0);
        }
    }

    [ButtonMethod]
    public void MuteMusic()
    {
        if (isMusicMuted)
        {
            isMusicMuted =false;
            mainMixer.SetFloat("VolumeMusic", 0);
            PlayerPrefs.SetInt("isMusicMuted", 0);
        }
        else
        {
            isMusicMuted = true;
            mainMixer.SetFloat("VolumeMusic", -80);
            PlayerPrefs.SetInt("isMusicMuted", 1);
        }
    }

    [ButtonMethod]
    public void MuteSFX()
    {
        if (isSFXMuted)
        {
            isSFXMuted = false;
            mainMixer.SetFloat("VolumeSFX", 0);
            PlayerPrefs.SetInt("isSFXMuted", 0);
        }
        else
        {
            isSFXMuted = true;
            mainMixer.SetFloat("VolumeSFX", -80);
            PlayerPrefs.SetInt("isSFXMuted", 1);
        }
    }
}
