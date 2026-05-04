using UnityEngine;

public class CurtainDraft : MonoBehaviour
{
    Cloth cloth;
    bool active;
    void Start()
    {
        cloth = GetComponent<Cloth>();
    }

    void Update()
    {
        if (GameManager.Instance.CurtainDraft && !active)
        {
            active = true;
            cloth.randomAcceleration = new Vector3(0f, 7f, 0f);
        }
        else if (!GameManager.Instance.CurtainDraft && active)
        {
            active = false;
            cloth.randomAcceleration = Vector3.zero;
        }
    }
}
