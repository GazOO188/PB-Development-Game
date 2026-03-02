using UnityEngine;

[CreateAssetMenu(fileName = "TooltipData", menuName = "ScriptableObjects/Tooltip Data")]
public class TooltipData : ScriptableObject
{
    [Header("Tooltip Content")]
    public string title;
    public Sprite titleImage;
    [TextArea(3, 6)]
    public string description;

    [Header("3D Model Settings")]
    public GameObject modelPrefab;

    // NEW: Adjust this to move the model up/down/left/right in the camera view
    public Vector3 modelPositionOffset = Vector3.zero;

    // Used to rotate the model to face the camera correctly
    public Vector3 modelRotation = new Vector3(0, 0, 0);

    public float modelScale = 1f;

    [Header("Input Hint")]
    public string actionKey = "E";
    public string actionText = "Interact";

    [Header("Visual Settings")]
    public Color backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.9f);
    public Color textColor = Color.white;
}