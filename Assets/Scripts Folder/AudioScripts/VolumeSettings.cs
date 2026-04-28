using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider ambienceSlider;

    [Header("Panel")]
    [SerializeField] private GameObject volumePanel; // the panel that shows/hides

    private bool isOpen = false;

    void Start()
    {
        if (musicSlider != null)
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
            musicSlider.onValueChanged.AddListener(OnMusicChanged);
            AudioManager.Instance.SetMusicVolume(musicSlider.value);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
            sfxSlider.onValueChanged.AddListener(OnSFXChanged);
            AudioManager.Instance.SetSFXVolume(sfxSlider.value);
        }

        if (ambienceSlider != null)
        {
            ambienceSlider.value = PlayerPrefs.GetFloat("AmbienceVolume", 1f);
            ambienceSlider.onValueChanged.AddListener(OnAmbienceChanged);
            AudioManager.Instance.SetAmbienceVolume(ambienceSlider.value);
        }

        volumePanel.SetActive(false);
    }

    // Call this from your volume icon button's OnClick()
    public void ToggleVolumePanel()
    {
        isOpen = !isOpen;
        volumePanel.SetActive(isOpen);
    }

    private void OnMusicChanged(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    private void OnSFXChanged(float value)
    {
        AudioManager.Instance.SetSFXVolume(value);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    private void OnAmbienceChanged(float value)
    {
        AudioManager.Instance.SetAmbienceVolume(value);
        PlayerPrefs.SetFloat("AmbienceVolume", value);
    }
}