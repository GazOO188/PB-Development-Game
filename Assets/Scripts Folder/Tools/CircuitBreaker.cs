using UnityEngine;

public class CircuitBreaker : MonoBehaviour
{
    public GameObject circuitPanel;
    [SerializeField] GameObject[] breakers;
    int index = 0;
    public bool active = true;
    public bool playerDetected;
    public bool singleReplaced, doubleReplaced;
    public bool complete = false;
    public bool singlePanelComplete, doublePanelComplete;
    void Start()
    {
        circuitPanel.GetComponent<Renderer>().material.color = Color.white;
    }

    void Update()
    {
        active = PlayerInventory.Instance.currentItem != null &&
                 PlayerInventory.Instance.currentItem.itemName == "Circuit Breaker";

        circuitPanel.GetComponent<Outline>().enabled = active && PlayerController.Instance.playerControl;

        if (!active)
        {
            for (int i = 0; i < 2; i++)
            {
                if (circuitPanel.GetComponent<Renderer>().material.color != Color.gray)
                    circuitPanel.GetComponent<Renderer>().material.color = Color.white;
            }
            return;
        }
        else
        {
            if (singleReplaced && doubleReplaced) return;

            if (breakers[0].activeInHierarchy && Input.GetKeyDown(KeyCode.Alpha2) && !doubleReplaced)
            {
                breakers[0].SetActive(false);
                breakers[1].SetActive(true);
                doublePanelComplete = true;
            }

            if (breakers[1].activeInHierarchy && Input.GetKeyDown(KeyCode.Alpha1) && !singleReplaced)
            {
                breakers[1].SetActive(false);
                breakers[0].SetActive(true);
                singlePanelComplete = true;
            }
        }
    }

    public void UpdateCircuit(GameObject panel)
    {
        /*panel.GetComponent<Renderer>().material.color = Color.gray;
        PlayerInventory.Instance.panelIndex++;
        index++;
        if (index == 1) circuits[1].GetComponent<Renderer>().material.color = Color.green;
        if (index == 2)
        {
            complete = true;
            PlayerInventory.Instance.currentItem = null;
        }*/
    }
}
