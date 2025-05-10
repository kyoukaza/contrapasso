using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    [Header("Mixer & Exposed Parameters")]
    public AudioMixer audioMixer;

    [Header("Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("UI")]
    public GameObject settingsPanel;

    private const string MASTER_KEY = "Master";
    private const string MUSIC_KEY = "Music";
    private const string SFX_KEY = "SFX";

    private const float BASE_MASTER_DB = 0f;
    private const float BASE_MUSIC_DB = -20f;
    private const float BASE_SFX_DB = -10f;

    void Start()
    {
        float masterValue = PlayerPrefs.GetFloat(MASTER_KEY, 1f);
        float musicValue = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxValue = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        ApplyVolume(masterValue, "Master", BASE_MASTER_DB);
        ApplyVolume(musicValue, "Music", BASE_MUSIC_DB);
        ApplyVolume(sfxValue, "SFX", BASE_SFX_DB);

        if (masterSlider != null) masterSlider.value = masterValue;
        if (musicSlider != null) musicSlider.value = musicValue;
        if (sfxSlider != null) sfxSlider.value = sfxValue;

        if (masterSlider != null) masterSlider.onValueChanged.AddListener(SetMasterVolume);
        if (musicSlider != null) musicSlider.onValueChanged.AddListener(SetMusicVolume);
        if (sfxSlider != null) sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMasterVolume(float value)
    {
        ApplyVolume(value, "Master", BASE_MASTER_DB);
        PlayerPrefs.SetFloat(MASTER_KEY, value);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float value)
    {
        ApplyVolume(value, "Music", BASE_MUSIC_DB);
        PlayerPrefs.SetFloat(MUSIC_KEY, value);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float value)
    {
        ApplyVolume(value, "SFX", BASE_SFX_DB);
        PlayerPrefs.SetFloat(SFX_KEY, value);
        PlayerPrefs.Save();
    }

    private void ApplyVolume(float sliderValue, string mixerParameter, float baseDb)
    {
        float dbValue = SliderToDb(sliderValue, baseDb);
        audioMixer.SetFloat(mixerParameter, dbValue);
    }

    private float SliderToDb(float sliderValue, float baseDb)
    {
        if (sliderValue <= 0.0001f) return -80f;
        return baseDb + Mathf.Log10(sliderValue) * 20f;
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
}
