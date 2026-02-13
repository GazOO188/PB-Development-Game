using UnityEngine;
using UnityEngine.UI;

public class ChangeLanguage : MonoBehaviour
{
    [SerializeField] Toggle otherLanguage;
    Toggle currentLanguage;

    void Start()
    {
        currentLanguage = GetComponent<Toggle>();
    }

    void Update()
    {

    }

    public void OnToggle()
    {
        if (!otherLanguage.isOn && !currentLanguage.isOn)
        {
            currentLanguage.isOn = true;
        }
        else if (otherLanguage.isOn && currentLanguage.isOn)
        {
            GameManager.Instance.ToggleLanguage();
            otherLanguage.isOn = false;
        }
    }
}
