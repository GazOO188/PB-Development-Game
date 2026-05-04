using UnityEngine;
using System.Collections.Generic;


public class Outlet : MonoBehaviour
{
    [SerializeField] WorkPhaseTimer timer;
    public List<GameObject> outlets = new List<GameObject>();
    int index = 0;
    public bool active = true;
    public bool canBeTested = false;
    public bool outletTested = false;
    public bool complete = false;
    Outline outline;
    void Start()
    {
        for (int i = 0; i < outlets.Count; i++)
            outlets[i].GetComponent<Renderer>().material.color = Color.gray;

        outline = GetComponentInChildren<Outline>();
        outline.enabled = false;
    }

    void Update()
    {
        if (timer.TaskOneDisplayed && !canBeTested) canBeTested = true;

        UpdateOutline();

        active = PlayerInventory.Instance.currentItem != null &&
                 PlayerInventory.Instance.currentItem.itemName == "Outlet";

        if (!active)
        {
            for (int i = 0; i < outlets.Count; i++)
            {
                if (outlets[i].GetComponent<Renderer>().material.color != Color.white)
                    outlets[i].GetComponent<Renderer>().material.color = Color.gray;
            }
            return;
        }
        else
        {
            //if (complete) Debug.Log("YOU DID IT");
        }
    }

    void UpdateOutline()
    {
        if (PlayerInventory.Instance.currentItem == null || complete || !timer.TaskOneDisplayed)
        {
            if (outline.enabled) outline.enabled = false;
            return;
        }

        if (!outletTested)
        {
            if (PlayerInventory.Instance.currentItem.itemName == "Outlet Tester")
            {
                if (!outline.enabled) outline.enabled = true;
                if (outline.OutlineColor != Color.green) outline.OutlineColor = Color.green;
            }
            else
            {
                if (outline.enabled) outline.enabled = false;
            }
        }
        else
        {
            if (PlayerInventory.Instance.currentItem.itemName == "Outlet")
            {
                if (!outline.enabled) outline.enabled = true;
                if (outline.OutlineColor != Color.green) outline.OutlineColor = Color.green;
            }
            else
            {
                if (outline.enabled) outline.enabled = false;
            }

        }
    }

    public void UpdateOutlet(GameObject currentOutlet)
    {
        if (!timer.TaskOneDisplayed || !outletTested) return;

        foreach (GameObject outlet in outlets)
        {
            if (currentOutlet == outlet)
            {
                outlet.GetComponent<Renderer>().material.color = Color.white;
                outlet.name = "Working Outlet";
                index++;
            }
        }

        if (index == outlets.Count)
        {
            PlayerInventory.Instance.currentItem = null;
            complete = true;
            //Debug.Log("Did");
        }
    }
}
