using UnityEngine;

public class InspectableObject : MonoBehaviour
{
    [SerializeField] private SoundLibrary sounds;
    [SerializeField] private string inspectSound = "inspect_click";
    [SerializeField] private string problemSound = "problem_found";

    public void OnInspect()
    {
        var e = sounds.Get(inspectSound);
        AudioManager.Instance.PlaySFXAt(
            e.variants[Random.Range(0, e.variants.Length)],
            transform.position, e.volume
        );
    }

    public void OnProblemFound()
    {
        var e = sounds.Get(problemSound);
        AudioManager.Instance.PlaySFXAt(
            e.variants[Random.Range(0, e.variants.Length)],
            transform.position, e.volume
        );
    }
}