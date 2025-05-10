using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsLoader : MonoBehaviour
{
    public AudioMixer audioMixer;

    void Start()
    {
        float masterDb = PlayerPrefs.GetFloat("MasterVolumeDb", 0f);
        float musicDb = PlayerPrefs.GetFloat("MusicVolumeDb", -10f);
        float sfxDb = PlayerPrefs.GetFloat("SFXVolumeDb", -10f);

        audioMixer.SetFloat("MasterVolume", masterDb);
        audioMixer.SetFloat("MusicVolume", musicDb);
        audioMixer.SetFloat("SFXVolume", sfxDb);
    }
}
