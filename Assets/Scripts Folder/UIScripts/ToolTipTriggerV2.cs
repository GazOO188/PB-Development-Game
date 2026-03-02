using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToolTipTriggerV2 : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private float maxDetectionDistance = 2f;
    [SerializeField] private LayerMask interactableLayers;
    private float distance;
    private bool showDebugRay = true;

    [Header("Tooltip Settings")]
    [SerializeField] public GameObject ToolTipPanel;
    [SerializeField] private float fadeInDuration = 0.5f;
    [SerializeField] private float fadeOutDuration = 0.3f;
    [SerializeField] private CanvasGroup tooltipCanvasGroup;
    [SerializeField] private TooltipUIManager tooltipUIManager;

    [Header("Tag-Specific Tooltips")]
    [SerializeField] private List<TagTooltipMapping> tagTooltips = new List<TagTooltipMapping>();

    // Dictionary to map tags to tooltip data
    private Dictionary<string, TooltipData> tagTooltipDict = new Dictionary<string, TooltipData>();

    private bool isToolTipActive = false;
    private Coroutine currentFadeCoroutine;
    private float toolTipOpenTime;
    private string currentTag = "";
    public AudioClip clip;
    public InputHandler inputHandler;
    [System.Serializable]
    public class TagTooltipMapping
    {
        public string tagName;
        public TooltipData tooltipData;
    }

    private void Start()
    {
        // Build dictionary for fast tag lookup
        foreach (var mapping in tagTooltips)
        {
            if (!string.IsNullOrEmpty(mapping.tagName))
            {
                tagTooltipDict[mapping.tagName] = mapping.tooltipData;
                Debug.Log($"Registered tooltip for tag: {mapping.tagName}");
            }
        }
    }

    private void Update()
    {
        if (!isToolTipActive)
        {
            DetectInteractable();
        }

        if (isToolTipActive)
        {
            if (inputHandler._interact.WasPressedThisFrame())
            {
                CloseToolTip();
            }
        }


    }

    private void DetectInteractable()
    {
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;

        RaycastHit hit;

        // 1. Raycast Check
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, maxDetectionDistance, interactableLayers))
        {
            distance = hit.distance;
            GameObject hitObject = hit.collider.gameObject;
            string hitTag = hitObject.tag;

            // DEBUG 1: Did we hit anything at all?
            Debug.Log($"Hit Object: {hitObject.name} | Tag: {hitTag} |");

            // 2. Dictionary Check
            if (tagTooltipDict.ContainsKey(hitTag))
            {
                // DEBUG 2: Tag is valid. Checking Manager...
                bool isActive = true;

                if (isActive)
                {
                    if (currentTag != hitTag)
                    {
                        ShowTooltip(tagTooltipDict[hitTag], hitTag);
                        currentTag = hitTag;
                    }
                }
                else
                {
                    // If this prints, the code works but the Manager thinks you already finished this tutorial.
                    Debug.Log($"Manager says tutorial for tag '{hitTag}' is INACTIVE (Already done?). Press 'R' to reset.");
                }
            }
            else
            {
                // DEBUG 3: If this prints for an object you expect to work, 
                // it means the tag string in the Inspector doesn't match the tag on the object EXACTLY.
                Debug.LogWarning($"Hit '{hitObject.name}' with tag '{hitTag}', but '{hitTag}' is not in the Dictionary!");
            }
        }

        if (showDebugRay)
        {
            Color rayColor = Physics.Raycast(rayOrigin, rayDirection, maxDetectionDistance) ? Color.green : Color.red;
            Debug.DrawRay(rayOrigin, rayDirection * maxDetectionDistance, rayColor);
        }

    }

    private void ShowTooltip(TooltipData data, string tagName)
    {
        if (ToolTipPanel != null && tooltipCanvasGroup != null && tooltipUIManager != null)
        {
            tooltipUIManager.UpdateTooltipContent(data);

            toolTipOpenTime = Time.unscaledTime;
            isToolTipActive = true;
            ToolTipPanel.SetActive(true);

            if (currentFadeCoroutine != null)
            {
                StopCoroutine(currentFadeCoroutine);
            }

            currentFadeCoroutine = StartCoroutine(FadeCanvasGroup(tooltipCanvasGroup, 0f, 1f, fadeInDuration));

            Debug.Log($"Showing tooltip for tag: {tagName}");
        }
        else
        {
            Debug.LogWarning("Missing tooltip references! Check inspector.");
        }
    }

    private void CloseToolTip()
    {
        if (currentFadeCoroutine != null)
        {
            if (isToolTipActive)
            {
                StopCoroutine(currentFadeCoroutine);
            }

            StartCoroutine(FadeOutAndClose());
        }
    }

    private IEnumerator FadeOutAndClose()
    {
        yield return StartCoroutine(FadeCanvasGroup(tooltipCanvasGroup, 1f, 0f, fadeOutDuration));

        if (tooltipUIManager != null)
        {
            // CHANGED: Clean up the 3D model
            tooltipUIManager.ClearModel();
        }

        Time.timeScale = 1f;
        isToolTipActive = false;
        ToolTipPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentTag = "";
    }
    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsed = 0f;
        cg.alpha = start;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsed / duration);
            yield return null;
        }

        cg.alpha = end;
    }

    public void ResetTooltip()
    {
        // Optional: Reset the manager data if you press R
        if (TutorialManager.Instance != null)
        {
            TutorialManager.Instance.ResetTutorials();
        }

        currentTag = "";
        if (ToolTipPanel != null)
        {
            ToolTipPanel.SetActive(false);
            isToolTipActive = false;
        }
        Debug.Log("Reset Tooltips via Manager");
    }
}