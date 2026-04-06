// using UnityEngine;
// using UnityEngine.UI;
// using System.Collections;
// using System;
// using System.Collections.Generic;
// public class ItemSelect : MonoBehaviour
// {
//     PlayerInventory inventory;
//     public string itemName;
//     Image sprite;
//     bool canSelectItem;
//     RectTransform rt;
//     Vector2 startPosition;
//     Vector2[] targetPositions = new Vector2[3];
//     bool equipped;
//     bool inPosition = true;
//     ConfirmButton confirm;
//     void Start()
//     {
//         sprite = GetComponent<Image>();
//         rt = GetComponent<RectTransform>();
//         startPosition = rt.anchoredPosition;

//         targetPositions[0] = new Vector2(325f, -303f);
//         targetPositions[1] = new Vector2(430f, -303f);
//         targetPositions[2] = new Vector2(535f, -303f);

//         confirm = GameObject.Find("Confirm Button").GetComponent<ConfirmButton>();
//     }

//     void Update()
//     {
//         if (canSelectItem && Input.GetMouseButtonDown(0) && inPosition && !PlayerInventory.Instance.itemIsMoving)
//         {
//             if (!equipped)
//                 StartCoroutine(MoveToItemSlot());
//             else
//                 StartCoroutine(RemoveFromItemSlot());
//         }
//     }

//     IEnumerator MoveToItemSlot()
//     {
//         PlayerInventory.Instance.itemIsMoving = true;
//         inPosition = false;

//         Vector2 start = rt.anchoredPosition;
//         Vector2 target = targetPositions[PlayerInventory.Instance.inventory.Count];

//         float elapsedTime = 0f;
//         float duration = Vector2.Distance(start, target) / 500.0f;

//         while (elapsedTime < duration)
//         {
//             float time = elapsedTime / duration;
//             rt.anchoredPosition = Vector2.Lerp(start, target, time);
//             elapsedTime += Time.deltaTime;
//             yield return null;
//         }

//         rt.anchoredPosition = target;
//         PlayerInventory.Instance.itemIsMoving = false;
//         PlayerInventory.Instance.inventory.Add(itemName);
//         confirm.itemNumber++;
//         inPosition = true;
//         equipped = true;
//     }

//     IEnumerator RemoveFromItemSlot()
//     {
//         PlayerInventory.Instance.itemIsMoving = true;

//         inPosition = false;

//         Vector2 start = rt.anchoredPosition;
//         Vector2 target = startPosition;

//         float elapsedTime = 0f;
//         float duration = Vector2.Distance(start, target) / 500.0f;

//         while (elapsedTime < duration)
//         {
//             float time = elapsedTime / duration;
//             rt.anchoredPosition = Vector2.Lerp(start, target, time);
//             elapsedTime += Time.deltaTime;
//             yield return null;
//         }

//         rt.anchoredPosition = target;
//         PlayerInventory.Instance.itemIsMoving = false;
//         PlayerInventory.Instance.inventory.Remove(itemName);
//         confirm.itemNumber--;
//         inPosition = true;
//         equipped = false;

//     }

//     public void MouseOver()
//     {
//         //if (PlayerInventory.Instance.inventory.Count < 3 || equipped) canSelectItem = true;
//         //if (sprite.color != Color.green) sprite.color = Color.green;
//     }

//     public void MouseExit()
//     {
//         //canSelectItem = false;
//         //if (sprite.color != Color.white) sprite.color = Color.white;
//     }
// }
