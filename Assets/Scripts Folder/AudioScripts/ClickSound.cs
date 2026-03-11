using UnityEngine;

public class ClickSound : MonoBehaviour
{
    [SerializeField] private SoundLibrary sounds;

    public void PlayClick()
    {
        var entry = sounds.Get("ui_click");
        AudioManager.Instance.PlaySFX(
            entry.variants[Random.Range(0, entry.variants.Length)], //AudioClip
            entry.volume
            //Pitch not passed to defaults to 1.0f.
        );
    }
}