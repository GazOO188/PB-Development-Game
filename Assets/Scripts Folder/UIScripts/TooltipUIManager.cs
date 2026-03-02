using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipUIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Image titleImage;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI actionText;
    [SerializeField] private Image backgroundImage;

    [Header("3D Model References")]
    [SerializeField] private Transform modelStageParent;
    [SerializeField] private RawImage renderTextureImage;

    private TooltipData currentData;
    private GameObject currentModelInstance;

    public void UpdateTooltipContent(TooltipData data)
    {
        if (data == null) return;

        currentData = data;

        // UI Text Updates
        if (titleText != null) titleText.text = data.title;
        if (descriptionText != null) descriptionText.text = data.description;
        if (actionText != null) actionText.text = $"Press [{data.actionKey}] to {data.actionText}";
        if (backgroundImage != null) backgroundImage.color = data.backgroundColor;

        ShowModel(data);
    }

    private void ShowModel(TooltipData data)
    {
        ClearModel();

        if (data.modelPrefab != null && modelStageParent != null)
        {
            currentModelInstance = Instantiate(data.modelPrefab, modelStageParent);

            // --- APPLY TRANSFORMS HERE ---

            // 1. Position: Apply the offset relative to the parent
            currentModelInstance.transform.localPosition = data.modelPositionOffset;

            // 2. Rotation: Apply the specific rotation
            currentModelInstance.transform.localRotation = Quaternion.Euler(data.modelRotation);

            // 3. Scale: Apply scale
            currentModelInstance.transform.localScale = Vector3.one * data.modelScale;

            if (renderTextureImage != null) renderTextureImage.enabled = true;

            SetLayerRecursively(currentModelInstance, modelStageParent.gameObject.layer);
        }
        else
        {
            if (renderTextureImage != null) renderTextureImage.enabled = false;
        }
    }

    public void ClearModel()
    {
        if (currentModelInstance != null)
        {
            Destroy(currentModelInstance);
            currentModelInstance = null;
        }
    }

    private void Update()
    {
        // Optional: Keep the subtle spin, but relative to the initial rotation
        if (currentModelInstance != null)
        {
            // If you want it to spin ON TOP of the set rotation:
            currentModelInstance.transform.Rotate(Vector3.up * 30f * Time.unscaledDeltaTime, Space.Self);
        }
    }

    private void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (obj == null) return;
        obj.layer = newLayer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}