using UnityEngine;

public class ClickSound : MonoBehaviour
{
    [SerializeField] private SoundLibrary sounds;

    public void PlayClickSound1()
    {
        var entry = sounds.Get("howtoplay_click");
        AudioManager.Instance.PlaySFX(
            entry.variants[Random.Range(0, entry.variants.Length)], //AudioClip
            entry.volume
        //Pitch not passed to defaults to 1.0f.
        );
    }

    public void PlayClickSound2()
    {
        var entry = sounds.Get("settings_click");
        AudioManager.Instance.PlaySFX(
            entry.variants[Random.Range(0, entry.variants.Length)], //AudioClip
            entry.volume
        //Pitch not passed to defaults to 1.0f.
        );
    }

    public void PlayAnswerClick()
    {
        var entry = sounds.Get("answer_click");
        AudioManager.Instance.PlaySFX(
            entry.variants[Random.Range(0, entry.variants.Length)], //AudioClip
            entry.volume
        //Pitch not passed to defaults to 1.0f.
        );
    }

    public void PlayDenyClick()
    {
        var entry = sounds.Get("deny_click");
        AudioManager.Instance.PlaySFX(
            entry.variants[Random.Range(0, entry.variants.Length)], //AudioClip
            entry.volume
        //Pitch not passed to defaults to 1.0f.
        );
    }
}