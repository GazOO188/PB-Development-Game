using UnityEngine;
using Unity.UI;
using TMPro;

public class ConfirmSelection : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    bool canClick;
    void Start()
    {

    }

    void Update()
    {
        if (canClick && Input.GetMouseButtonDown(0))
            SceneLoader.Instance.LoadNextScene();
    }

    public void MouseOver()
    {
        canClick = true;
        text.color = Color.yellow;
    }

    public void MouseExit()
    {
        canClick = false;
        text.color = Color.white;
    }
}
