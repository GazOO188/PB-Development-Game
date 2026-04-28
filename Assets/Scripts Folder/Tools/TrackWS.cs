using UnityEngine;

public class TrackWS : MonoBehaviour
{
    [SerializeField] CheckForWS[] tracker;
    public bool complete;
    void Update()
    {
        if (tracker[0].weatherStripDetected && tracker[1].weatherStripDetected && !complete)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("WEATHER STRIP SET");
                complete = true;
            }
        }
    }
}
