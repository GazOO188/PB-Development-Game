using UnityEngine;

public class NPCSound : MonoBehaviour
{
    [SerializeField] private SoundLibrary sounds;
    [SerializeField] private string soundKey = "npc_interact";

    public void PlayInteractSound()
    {
        var entry = sounds.Get(soundKey);
        if (entry == null) { Debug.LogError($"{soundKey} not found!"); return; }

        AudioManager.Instance.PlaySFXAt(
            entry.variants[Random.Range(0, entry.variants.Length)],
            transform.position,
            entry.volume
        );
    }
}