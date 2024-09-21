using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public List<AudioClip> audioClips = new List<AudioClip>();

    private AudioSource audioSource;

    public float defaultVolume = 0.5f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
       
        DontDestroyOnLoad(gameObject); //Impido q se destruya en escena.
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = defaultVolume;
        PlaySound(12);
    }

    public void PlaySound(int soundIndex)
    {
        if (soundIndex >= 0 && soundIndex < audioClips.Count)
        {
            audioSource.clip = audioClips[soundIndex];
            audioSource.Play();
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp01(volume);
    }
    public void StopSounds()
    {
        if (audioSource.isPlaying==true)
        {
            audioSource.Stop();
        }
    }
    public void PlayCombatSounds(int soundIndex)
    {
        if (soundIndex >= 0 && soundIndex < audioClips.Count)
        {
            audioSource.PlayOneShot(audioClips[soundIndex]);
        }
    }
}
