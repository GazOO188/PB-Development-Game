using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class CircuitSwitch : MonoBehaviour
{
    [SerializeField] CircuitBreaker circuitManager;
    [SerializeField] GameObject playerBreakers;
    [SerializeField] Quaternion on, off;
    bool isMoving, isHovering;
    public bool isOn, isSinglePanel, isDamaged, needsDoublePanel;
    [Tooltip("Leave empty if 'Needs Double Panel' is false.")]
    [SerializeField] GameObject replacementPanel;
    [Tooltip("Leave empty if 'Is Damaged' is false.")]
    [SerializeField] GameObject replacementSinglePanel;
    Outline outline;

    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    void Update()
    {
        outline.enabled = isHovering;

        if (isHovering && Input.GetMouseButtonDown(0) && !isMoving)
        {
            StartCoroutine(MoveSwitch());
        }

        NewCircuits();
    }

    void NewCircuits()
    {
        if (isHovering && Input.GetMouseButtonDown(1) && !isMoving && isSinglePanel && needsDoublePanel
            && PlayerInventory.Instance.currentTool is PlayerInventory.AllTools.CircuitBreaker
            && playerBreakers.transform.GetChild(1).gameObject.activeInHierarchy)
        {
            replacementPanel.SetActive(true);
            playerBreakers.transform.GetChild(1).gameObject.SetActive(false);
            circuitManager.doubleReplaced = true;
            if (!circuitManager.singleReplaced) playerBreakers.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        if (isHovering && Input.GetMouseButtonDown(1) && !isMoving && isSinglePanel && isDamaged
            && PlayerInventory.Instance.currentTool is PlayerInventory.AllTools.CircuitBreaker
            && playerBreakers.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            replacementSinglePanel.SetActive(true);
            playerBreakers.transform.GetChild(0).gameObject.SetActive(false);
            circuitManager.singleReplaced = true;
            if (!circuitManager.doubleReplaced) playerBreakers.transform.GetChild(1).gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    IEnumerator MoveSwitch()
    {
        isMoving = true;

        Quaternion start = transform.rotation;
        Quaternion end = isOn ? off : on;

        float elapsed = 0f;
        float duration = 0.25f;

        while (elapsed < duration)
        {
            float time = elapsed / duration;
            transform.rotation = Quaternion.Lerp(start, end, time);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = end;

        isOn = !isOn;

        isMoving = false;
    }

    public void MouseEnter()
    {
        isHovering = true;
    }

    public void MouseExit()
    {
        isHovering = false;
    }
}
