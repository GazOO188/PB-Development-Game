using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource ambienceSource;
    [SerializeField] private AudioSource uiSource;

    [Header("Settings")]
    [SerializeField] private int sfxPoolSize = 10;

    private List<AudioSource> sfxPool = new();
    private Dictionary<string, AudioClip> clipCache = new();

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeSFXPool();
    }

    void InitializeSFXPool()
    {
        for (int i = 0; i < sfxPoolSize; i++)
        {
            var go = new GameObject($"SFX_Pool_{i}");
            go.transform.SetParent(transform);
            sfxPool.Add(go.AddComponent<AudioSource>());
        }
    }

    // --- Music & Ambience ---

    public void PlayMusic(AudioClip clip, float fadeTime = 1f)
    {
        if (musicSource.clip == clip) return;
        StartCoroutine(FadeSwap(musicSource, clip, fadeTime));
    }

    public void PlayAmbience(AudioClip clip)
    {
        ambienceSource.clip = clip;
        ambienceSource.loop = true;
        ambienceSource.Play();
    }

    // --- 2D UI / Non-Spatial SFX ---

    public void PlaySFX(AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        var source = GetAvailableSource(); //Goes through pool of premade audio sources and finds one that is not currently playing, if all are playing it will steal the first one in the list.
        if (source == null) return;
        source.spatialBlend = 0f; // Makes the sound 2D (non-spatial), plays at equal volume in both ears regardless of listener position.
        source.volume = volume; 
        source.pitch = pitch;
        source.clip = clip;
        source.Play(); //After setting all the properties, we call Play() to start the sound. The audio source will automatically stop when the clip finishes playing, making it available for reuse in the pool.
    }

    // --- 3D Spatial SFX (key for your game!) ---

    public void PlaySFXAt(AudioClip clip, Vector3 position, float volume = 1f,
                          float minDistance = 1f, float maxDistance = 20f)
    {
        var source = GetAvailableSource();
        if (source == null) return;
        source.transform.position = position;
        source.spatialBlend = 1f; // Makes the sound fully 3D (spatial), volume and panning will depend on listener position relative to source.
        source.rolloffMode = AudioRolloffMode.Logarithmic;
        source.minDistance = minDistance;
        source.maxDistance = maxDistance;
        source.volume = volume;
        source.clip = clip;
        source.Play();
    }

    // --- Helpers ---

    private AudioSource GetAvailableSource()
    {
        foreach (var s in sfxPool)
            if (!s.isPlaying) return s;
        return sfxPool[0]; // steal oldest
    }

    private System.Collections.IEnumerator FadeSwap(AudioSource src, AudioClip newClip, float duration)
    {
        float start = src.volume;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            src.volume = Mathf.Lerp(start, 0, t / duration);
            yield return null;
        }
        src.clip = newClip;
        src.Play();
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            src.volume = Mathf.Lerp(0, start, t / duration);
            yield return null;
        }
    }
}