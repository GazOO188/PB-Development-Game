using UnityEngine;

public class CheckForWS : MonoBehaviour
{
    public bool weatherStripDetected;
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Strip"))
        {
            weatherStripDetected = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Strip"))
        {
            weatherStripDetected = false;
        }
    }
}
