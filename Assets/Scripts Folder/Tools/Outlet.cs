using UnityEngine;
using System.Collections.Generic;


public class Outlet : MonoBehaviour
{
    [SerializeField] WorkPhaseTimer timer;
    public GameObject brokenOutlet, workingOutlet;
    int index = 0;
    public bool active = true;
    public bool canBeTested = false;
    public bool outletTested = false;
    public bool complete = false;
    Outline outline;
    void Start()
    {
        outline = GetComponentInChildren<Outline>();
        outline.enabled = false;
    }

    void Update()
    {
        if (timer.TaskOneDisplayed && !canBeTested) canBeTested = true;

        UpdateOutline();

        // active = PlayerInventory.Instance.currentItem != null &&
        //          PlayerInventory.Instance.currentItem.itemName == "Outlet";

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
                //if (outline.OutlineColor != Color.green) outline.OutlineColor = Color.green;
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
                //if (outline.OutlineColor != Color.green) outline.OutlineColor = Color.green;
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

        if (currentOutlet == brokenOutlet)
        {
            brokenOutlet.SetActive(false);
            workingOutlet.SetActive(true);
            //outlet.GetComponent<Renderer>().material.color = Color.white;
            //outlet.name = "Working Outlet";
            PlayerInventory.Instance.currentItem = null;
            complete = true;
            //index++;
        }
    }
}
