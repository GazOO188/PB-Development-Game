using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class ToolTipTrigger : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private float maxDetectionDistance = 2f;
    [SerializeField] private LayerMask fleshWallLayer;
    public float distance;

    [Header("Tooltip Settings")]
    [SerializeField] public GameObject ToolTipPanel;
    [SerializeField] private float fadeInDuration = 0.5f;
    [SerializeField] private float fadeOutDuration = 100f;
    [SerializeField] private CanvasGroup tooltipCanvasGroup;

    [Header("Debug")]
    [SerializeField] private bool showDebugRay = true;
    private bool hasShownDrillTooltip = false;
    private bool isToolTipActive = false;
    private Coroutine currentFadeCoroutine;
    private float toolTipOpenTime; //Keeps track of when the tooltip was opened

    private void Update()
    {
        if (!isToolTipActive)
        {
            DetectFleshWall();
        }

        if (isToolTipActive && Input.GetKeyDown(KeyCode.E) && Time.unscaledTime - toolTipOpenTime > .2f) //Subtract the current time minus the time the tooltip was opened to add slight delay before allowing it to be closed
        {
            CloseToolTip();
        }
    }

    private void DetectFleshWall()
    {
   
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, maxDetectionDistance, fleshWallLayer))
        {
            distance = hit.distance;
            Debug.Log($"Flesh wall hit! Distance: {distance:F2} units");

            if (!hasShownDrillTooltip)
            {
                ShowDrillTooltip();
                hasShownDrillTooltip = true;
            }
        }

        // Debug visualization
        if (showDebugRay)
        {
            Color rayColor = Physics.Raycast(rayOrigin, rayDirection, maxDetectionDistance, fleshWallLayer) ? Color.green : Color.red;
            Debug.DrawRay(rayOrigin, rayDirection * maxDetectionDistance, rayColor);
        }
    }

    private void ShowDrillTooltip()
    {
        if (ToolTipPanel != null && tooltipCanvasGroup != null)
        {
            toolTipOpenTime = Time.unscaledTime;
            Time.timeScale = 0;
            isToolTipActive = true;
            ToolTipPanel.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (currentFadeCoroutine != null)
            {
                StopCoroutine(currentFadeCoroutine);
            }

            currentFadeCoroutine = StartCoroutine(FadeCanvasGroup(tooltipCanvasGroup, 0f, 1f, fadeInDuration));

            Debug.Log("Showing drill tooltip!");
        }
    }

    private void CloseToolTip()
    {
        if (currentFadeCoroutine != null)
        {
            if(isToolTipActive)
            {
                StopCoroutine(currentFadeCoroutine);
            }

            StartCoroutine(FadeOutAndClose());
        }
    }

    private IEnumerator FadeOutAndClose()
    {
        yield return StartCoroutine(FadeCanvasGroup(tooltipCanvasGroup, 1f, 0f, fadeOutDuration));

        Time.timeScale = 1f;
        isToolTipActive = false;
        ToolTipPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("Tool tip closing.....");
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsed = 0f;
        cg.alpha = start;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime; // Use unscaled time so it works while paused
            cg.alpha = Mathf.Lerp(start, end, elapsed / duration);
            yield return null;
        }

        cg.alpha = end;
    }

    public void ResetTooltip() //call this when needed
    {
        hasShownDrillTooltip = false;
        if (ToolTipPanel != null)
        {
            ToolTipPanel.SetActive(false);
            isToolTipActive = false;
        }
    }
}