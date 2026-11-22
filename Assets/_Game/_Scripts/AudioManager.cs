using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    public AudioMixer audioMixer;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(musicSource);
        DontDestroyOnLoad(sfxSource);
    }
    

    public void SetVolume(string mixerName, float volume)
    {
        volume = Mathf.Log10(volume/100) * 20f;
        audioMixer.SetFloat(mixerName, volume);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (!clip) return;
        sfxSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return;
        musicSource.clip = clip;
        musicSource.Play();
    }

    public IEnumerator Crossfade(AudioClip next, float time)
    {
        float start = musicSource.volume;
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(start, 0f, t / time);
            yield return null;
        }
        musicSource.clip = next;
        musicSource.Play();
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(0f, start, t / time);
            yield return null;
        }
    }
}
