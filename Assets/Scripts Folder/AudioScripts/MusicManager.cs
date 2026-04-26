using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [SerializeField] private AudioSource normalSource;
    [SerializeField] private AudioSource tenseSource;
    [SerializeField] private float fadeSpeed = 1.5f;

    private bool isTense = false;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        // Start with normal music at full volume, tense at zero
        normalSource.volume = 1f;
        tenseSource.volume = 0f;
        normalSource.Play();
        tenseSource.Play(); // both play simultaneously, we just fade between them
    }

    public void SetTense(bool tense)
    {
        if (isTense == tense) return;
        isTense = tense;
        StopAllCoroutines();
        StartCoroutine(CrossFade(tense));
    }

    private IEnumerator CrossFade(bool toTense)
    {
        float elapsed = 0f;
        float startNormal = normalSource.volume;
        float startTense = tenseSource.volume;
        float targetNormal = toTense ? 0f : 1f;
        float targetTense = toTense ? 1f : 0f;

        while (elapsed < fadeSpeed)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeSpeed;
            normalSource.volume = Mathf.Lerp(startNormal, targetNormal, t);
            tenseSource.volume = Mathf.Lerp(startTense, targetTense, t);
            yield return null;
        }
    }
}