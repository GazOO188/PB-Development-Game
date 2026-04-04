using UnityEngine;
using System.Collections.Generic;


public class Outlet : MonoBehaviour
{
    public List<GameObject> outlets = new List<GameObject>();
    int index = 0;
    public bool active = true;
    public bool complete = false;
    public bool[] functional = new bool[4];
    void Start()
    {
        for (int i = 0; i < 4; i++)
            outlets[i].GetComponent<Renderer>().material.color = Color.gray;
    }

    void Update()
    {
        active = PlayerInventory.Instance.currentItem != null &&
                 PlayerInventory.Instance.currentItem.itemName == "Outlet";

        if (!active)
        {
            for (int i = 0; i < 4; i++)
            {
                if (outlets[i].GetComponent<Renderer>().material.color != Color.white)
                    outlets[i].GetComponent<Renderer>().material.color = Color.gray;
            }
            return;
        }
        else
        {
            if (complete) Debug.Log("YOU DID IT");
        }
    }

    public void UpdateOutlet(GameObject currentOutlet)
    {
        foreach (GameObject outlet in outlets)
        {
            if (currentOutlet == outlet)
            {
                outlet.GetComponent<Renderer>().material.color = Color.white;
                index++;
            }
        }

        if (index == 4)
        {
            PlayerInventory.Instance.currentItem = null;
            complete = true;
            Debug.Log("Did");
        }
    }
}
