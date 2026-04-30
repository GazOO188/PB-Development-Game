using UnityEngine;

public class CheckForFoam : MonoBehaviour
{
    // Check points
    [SerializeField] Transform[] checks;
    bool[] isHitting;
    [SerializeField] LayerMask foamLayer;
    public bool complete;

    void Start()
    {
        // Grab all children to use as check points
        checks = new Transform[transform.childCount];

        // bool for each child object
        isHitting = new bool[checks.Length];

        for (int i = 0; i < checks.Length; i++)
        {
            // Add children to array
            checks[i] = transform.GetChild(i);
        }
    }

    void Update()
    {
       // RaycastCheck();
        if (FoamDetected() && !complete)
        {
            Debug.Log("FOAM SET");
            complete = true;
        }
    }

    void RaycastCheck()
    {
        /// Draw a ray from each child and check to see if it is hitting foam ///
        for (int i = 0; i < checks.Length; i++)
        {
            Vector3 direction = -checks[i].transform.forward;
            Debug.DrawRay(checks[i].position, direction * 0.2f, Color.magenta);

            RaycastHit hit;
            if (Physics.Raycast(checks[i].position, direction, out hit, 0.5f, foamLayer))
            {
                if (hit.transform.CompareTag("Foam") && !isHitting[i]) isHitting[i] = true;
                else if (!hit.transform.CompareTag("Foam") && isHitting[i]) isHitting[i] = false;
            }
        }
    }

    public bool FoamDetected()
    {
        // Return true if all rays are detecting foam
        for (int i = 0; i < isHitting.Length; i++)
        {
            if (!isHitting[i]) return false;
        }

        return true;
    }
}
