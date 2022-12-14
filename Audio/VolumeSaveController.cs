using UnityEngine.UI;
using UnityEngine;

public class VolumeSaveController : MonoBehaviour
{
    [SerializeField]
    private Slider sfxVolumeSlider = null;
    [SerializeField]
    private Slider musicVolumeSlider = null;
    [SerializeField]
    private Text sfxVolumeText = null;
    [SerializeField]
    private Text musicVolumeText = null;
    private void Start()
    {
        LoadVolume();
    }

    public void SFXVolumeSlider(float volume)
    {
        sfxVolumeText.text = volume.ToString("0.0");
    }

    public void MusicVolumeSlider(float volume)
    {
        musicVolumeText.text = volume.ToString("0.0");
    }

    public void SaveVolumeButton()
    {
        float sfxVolumeValue = sfxVolumeSlider.value;
        float musicVolumeValue = musicVolumeSlider.value;
        PlayerPrefs.SetFloat("SFXVolumeValue", sfxVolumeValue);
        PlayerPrefs.SetFloat("MusicVolumeValue", musicVolumeValue);
        LoadVolume();
    }
    private void LoadVolume()
    {
        float sfxVolumeValue = PlayerPrefs.GetFloat("SFXVolumeValue");
        float musicVolumeValue = PlayerPrefs.GetFloat("MusicVolumeValue");
        sfxVolumeSlider.value = sfxVolumeValue;
        musicVolumeSlider.value = musicVolumeValue;
        //AudioListener.volume = volumeValue; Isto é para audio geral e não específico
    }
}
