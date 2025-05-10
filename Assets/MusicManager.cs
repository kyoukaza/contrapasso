using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Audio Settings")]
    public AudioClip ambientLoop;
    public AudioMixerGroup musicMixer;

    private AudioSource musicSource;

    void Awake()
    {

         if (SceneManager.GetActiveScene().name != "SampleScene")
    {
        Destroy(gameObject);
        return;
    }

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = ambientLoop;
        musicSource.outputAudioMixerGroup = musicMixer;
        musicSource.loop = true;
        musicSource.playOnAwake = false;
        musicSource.volume = 1f;
        musicSource.spatialBlend = 0f;
        musicSource.Play();
    }
}
