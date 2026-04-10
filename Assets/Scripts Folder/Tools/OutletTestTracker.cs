using UnityEngine;

public class OutletTestTracker : MonoBehaviour
{
    public int index = 0;
    public bool fin;

    void Update()
    {
        if (index == 2 && !fin) fin = true;
    }
}
