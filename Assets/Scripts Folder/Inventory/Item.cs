using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class Item : MonoBehaviour
{
    Vector2 startPosition;
    Image sprite;
    public bool inFront = true;
    bool canSelectItem, isHovering, mouseCheck;
    public string itemName;
    Vector3 startSize;
    Transform childSprite;
    void Start()
    {
        childSprite = transform.GetChild(0);
        sprite = childSprite.GetComponent<Image>();
        startSize = childSprite.localScale;
    }

    void Update()
    {
        if (sprite.color == Color.blue && PlayerInventory.Instance.currentItem != this)
            if (canSelectItem) sprite.color = Color.green;
            else sprite.color = Color.white;
        Debug.Log(transform.localScale);

        DetectPlayer();
        Select();

        if (!mouseCheck)
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
                mouseCheck = true;
        }

        if (isHovering && sprite.color != Color.blue)
        {
            childSprite.localScale = startSize + new Vector3
            (
                childSprite.localScale.x * Mathf.Sin(Time.time * 7f) * 0.15f,
                childSprite.localScale.y * Mathf.Sin(Time.time * 7f) * 0.15f,
                childSprite.localScale.z * Mathf.Sin(Time.time * 7f) * 0.15f
            );
        }
        else
        {
            if (Vector3.Distance(childSprite.localScale, startSize) > 0.01f)
                childSprite.localScale = Vector3.Lerp(childSprite.localScale, startSize, 0.5f);
            else if (childSprite.localScale != startSize)
                childSprite.localScale = startSize;
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
