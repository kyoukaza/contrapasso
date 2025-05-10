using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Mixer")]
    public AudioMixerGroup sfxMixer;

    [Header("SFX Clips")]
    public AudioClip footstepClip;
    public AudioClip itemPickupClip;
    public AudioClip itemDropClip;
    public AudioClip itemUseClip;
    public AudioClip trapTriggerClip;
    public AudioClip playerDamagedClip;
    public AudioClip playerScreechClip;
    public AudioClip ladderUseClip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        GameObject temp = new GameObject("SFX_" + clip.name);
        AudioSource source = temp.AddComponent<AudioSource>();
        source.clip = clip;
        source.outputAudioMixerGroup = sfxMixer;
        source.Play();

        Destroy(temp, clip.length);
    }

    public void PlayFootstep() => PlaySound(footstepClip);
    public void PlayItemPickup() => PlaySound(itemPickupClip);
    public void PlayItemDrop() => PlaySound(itemDropClip);
    public void PlayItemUse() => PlaySound(itemUseClip);
    public void PlayTrapTrigger() => PlaySound(trapTriggerClip);
    public void PlayPlayerDamaged() => PlaySound(playerDamagedClip);
    public void PlayPlayerScreech() => PlaySound(playerScreechClip);
    public void PlayLadderUse() => PlaySound(ladderUseClip);
}
