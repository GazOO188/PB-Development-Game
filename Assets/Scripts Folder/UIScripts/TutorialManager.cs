
using System.Collections.Generic;
using UnityEngine; // Removed System.Diagnostics to avoid ambiguity

public class TutorialManager : MonoBehaviour
{
    // Singleton pattern
    public static TutorialManager Instance;

    [System.Serializable]
    public class TutorialStep
    {
        public string stepName;
        public bool isActive = true;
    }

    [Header("Save Settings")]
    public string saveKey = "Tutorial_Progress_Save01";

    [Header("Tutorial Checklist")]
    public List<TutorialStep> tutorialSteps = new List<TutorialStep>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        LoadTutorialData();
    }

    public void CompleteTutorial(string targetName)
    {
        bool found = false;

        foreach (var step in tutorialSteps)
        {
            if (step.stepName == targetName)
            {
                if (step.isActive)
                {
                    step.isActive = false;

                    Debug.Log($"Tutorial '{targetName}' marked as Complete (in memory only).");
                }
                found = true;
                break;
            }
        }

        if (!found)
        {
            Debug.LogWarning($"TutorialManager: Could not find step '{targetName}'.");
        }
    }

    public bool IsTutorialActive(string targetName)
    {
        foreach (var step in tutorialSteps)
        {
            if (step.stepName == targetName)
                return step.isActive;
        }
        return false;
    }

    // ---------------- SAVE SYSTEM ---------------- //

    public void SaveTutorialData()
    {

    }

    public void LoadTutorialData()
    {

    }

    [ContextMenu("Reset Tutorials")]
    public void ResetTutorials()
    {
    }
}