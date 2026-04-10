using UnityEngine;

public class WorldClickSound : MonoBehaviour
{
    [SerializeField] private SoundLibrary sounds;
    [SerializeField] private string soundKey = "switch_click";

    public void PlaySound()
    {
        var entry = sounds.Get(soundKey);
        if (entry == null) { Debug.LogError($"{soundKey} not found!"); return; }
        if (entry.variants.Length == 0) { Debug.LogError("No variants assigned!"); return; }

        AudioManager.Instance.PlaySFXAt(
            entry.variants[Random.Range(0, entry.variants.Length)],
            transform.position,
            entry.volume
        );
    }
}