using UnityEngine;

public class ConfirmButton : MonoBehaviour
{
    [SerializeField] GameObject button;
    public int itemNumber = 0;

    void Update()
    {
        if (itemNumber == 3 && !button.activeInHierarchy)
            button.SetActive(true);
        else if (itemNumber < 3 && button.activeInHierarchy)
            button.SetActive(false);
    }
}
