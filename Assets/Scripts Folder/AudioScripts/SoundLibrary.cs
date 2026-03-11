using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundLibrary", menuName = "ScriptableObjects/SoundLibrary")]
public class SoundLibrary : ScriptableObject
{
    [System.Serializable]
    public class SoundEntry
    {
        public string key;
        public AudioClip[] variants; // random variation support
        [Range(0f, 1f)] public float volume = 1f;
        [Range(0.8f, 1.2f)] public float pitchVariance = 1f;
    }

    public SoundEntry[] sounds;
    private Dictionary<string, SoundEntry> lookup;

    public void Init()
    {
        lookup = new();
        foreach (var s in sounds) lookup[s.key] = s;
    }

    public SoundEntry Get(string key)
    {
        if (lookup == null) Init();
        return lookup.TryGetValue(key, out var s) ? s : null;
    }
}