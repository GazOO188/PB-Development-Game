using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class BreakerPlayerCheck : MonoBehaviour
{
    [SerializeField] GameObject circuitManager;
    bool playerDetected;
    [SerializeField] Transform camTarget;
    bool isMoving, usingBreaker;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject breakerCollider;


    [Header("TextMeshPro")]

    [SerializeField] public TextMeshProUGUI InteractText;


    [SerializeField] public CollisionInteractions CI;


    void Awake()
    {

        InteractText.enabled = false;


    }

    void Start()
    {
        breakerCollider.SetActive(false);
    }

    void Update()
    {
        if (usingBreaker)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ReturnControls();
                return;
            }
        }

        if (playerDetected)
        {
            if (Input.GetKeyDown(KeyCode.E) && !isMoving && !usingBreaker)
            {
                StartCoroutine(MoveToPosition(camTarget.position, camTarget.rotation));
            }
        }
    }

    IEnumerator MoveToPosition(Vector3 targetPos, Quaternion targetRot)
    {
        PlayerController.Instance.playerControl = false;
        isMoving = true;


        Vector3 start = Camera.main.transform.position;
        Quaternion rot = Camera.main.transform.rotation;

        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            float time = elapsed / duration;
            Camera.main.transform.position = Vector3.Lerp(start, targetPos, time);
            Camera.main.transform.rotation = Quaternion.Lerp(rot, targetRot, time);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.position = targetPos;
        Camera.main.transform.rotation = targetRot;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        panel.SetActive(true);
        isMoving = false;
        breakerCollider.SetActive(true);
        usingBreaker = true;
    }

    void ReturnControls()
    {
        usingBreaker = false;
        PlayerController.Instance.playerControl = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        breakerCollider.SetActive(false);
        panel.SetActive(false);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerDetected = true;

            InteractText.enabled = true;

            CI.InteractText.enabled = false;


        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerDetected = false;

            InteractText.enabled = false;

        }
    }
}
