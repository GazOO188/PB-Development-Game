using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class Item : MonoBehaviour
{
    Vector2 startPosition;
    Image sprite;
    public bool inFront;
    bool canSelectItem, isHovering, mouseCheck;
    public string itemName;
    void Start()
    {
        sprite = GetComponent<Image>();
    }

    void Update()
    {
        if (sprite.color == Color.blue && PlayerInventory.Instance.currentItem != this)
            if (canSelectItem) sprite.color = Color.green;
            else sprite.color = Color.white;

        DetectPlayer();
        Select();

        if (!mouseCheck)
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
                mouseCheck = true;
        }
    }

    void DetectPlayer()
    {
        if (isHovering && inFront && mouseCheck)
        {
            if (sprite.color == Color.white)
            {
                sprite.color = Color.green;
                canSelectItem = true;
            }
        }
        else if (sprite.color == Color.green)
        {
            sprite.color = Color.white;
            canSelectItem = false;
        }
        //Debug.Log(isHovering);
    }

    public void Select()
    {
        if (canSelectItem && Input.GetMouseButtonDown(0))
        {

            if (sprite.color != Color.blue)
            {
                sprite.color = Color.blue;
                PlayerInventory.Instance.currentItem = null;
                PlayerInventory.Instance.currentItem = this;
            }
            else
            {
                sprite.color = Color.green;
                PlayerInventory.Instance.currentItem = null;
            }
        }
    }

    public void MouseOver()
    {
        isHovering = true;
    }

    public void MouseExit()
    {
        isHovering = false;
    }

    public void CloseUI()
    {
        isHovering = false;
        mouseCheck = false;
        if (sprite.color == Color.green) sprite.color = Color.white;
    }
}
