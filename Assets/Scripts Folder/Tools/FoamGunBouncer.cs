using UnityEngine;

public class FoamGunBouncer : MonoBehaviour
{
    [SerializeField] SprayFoam foamGun;
    [SerializeField] GameObject bigFoam, littleFoam;
    Collider col;

    void Start()
    {
        col = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (col.enabled && PlayerInventory.Instance.currentItem != null && PlayerInventory.Instance.currentItem.name == "Foam Gun")
        {
            col.enabled = false;
        }
        else if (!col.enabled && !(PlayerInventory.Instance.currentItem != null && PlayerInventory.Instance.currentItem.name == "Foam Gun"))
        {
            col.enabled = true;
        }
    }
}
