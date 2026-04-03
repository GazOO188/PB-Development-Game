using UnityEngine;

public class CircuitBreaker : MonoBehaviour
{
    [SerializeField] GameObject[] circuits;
    int index = 0;
    public bool active = true;
    public bool complete = false;
    void Start()
    {
        for (int i = 0; i < 2; i++)
            circuits[i].GetComponent<Renderer>().material.color = Color.white;
    }

    void Update()
    {
        active = PlayerInventory.Instance.currentItem != null &&
                 PlayerInventory.Instance.currentItem.itemName == "Circuit Breaker";

        if (!active)
        {
            for (int i = 0; i < 2; i++)
            {
                if (circuits[i].GetComponent<Renderer>().material.color != Color.gray)
                    circuits[i].GetComponent<Renderer>().material.color = Color.white;
            }
            return;
        }
        else
        {
            if (index == 0 && circuits[0].GetComponent<Renderer>().material.color != Color.green)
                circuits[0].GetComponent<Renderer>().material.color = Color.green;
            else if (index == 1 && circuits[1].GetComponent<Renderer>().material.color != Color.green)
                circuits[01].GetComponent<Renderer>().material.color = Color.green;

            //if (complete) Debug.Log("YOU DID IT");
        }
    }

    public void UpdateCircuit(GameObject panel)
    {
        panel.GetComponent<Renderer>().material.color = Color.gray;
        PlayerInventory.Instance.panelIndex++;
        index++;
        if (index == 1) circuits[1].GetComponent<Renderer>().material.color = Color.green;
        if (index == 2)
        {
            complete = true;
            PlayerInventory.Instance.currentItem = null;
        }
    }
}
