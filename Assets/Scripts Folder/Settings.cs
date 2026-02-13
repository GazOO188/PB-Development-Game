using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] GameObject settings, settingsButton;
    [SerializeField] GameObject howToPlay, howToPlayButton;
    void Start()
    {

    }

    void Update()
    {

    }

    public void ToggleMenu()
    {
        if (settings.activeInHierarchy)
        {
            if (!howToPlayButton.activeInHierarchy) howToPlayButton.SetActive(true);
            settings.SetActive(false);
        }
        else
        {
            if (howToPlayButton.activeInHierarchy) howToPlayButton.SetActive(false);
            settings.SetActive(true);
        }
    }

    public void ToggleInstructions()
    {
        if (howToPlay.activeInHierarchy)
        {
            if (!settingsButton.activeInHierarchy) settingsButton.SetActive(true);
            howToPlay.SetActive(false);
        }
        else
        {
            if (settingsButton.activeInHierarchy) settingsButton.SetActive(false);
            howToPlay.SetActive(true);
        }
    }

}
