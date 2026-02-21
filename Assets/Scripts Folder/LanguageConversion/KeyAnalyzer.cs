using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyAnalyzer : MonoBehaviour
{
    //THIS SCRIPT WILL THEN BE ATTACHED TO GAMEOBJECTS WITH A TEXT COMPONENT//


    public string Word;

    public TextMeshProUGUI TextComponent;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (TextComponent == null)
        {
            Debug.LogError("TextComponent not assigned!");
            return;
        }

        TextComponent = GetComponent<TextMeshProUGUI>();

       
    }

    // Update is called once per frame
    void Update()
    {
       WordConversion();  

        
    }


    public void WordConversion()
    {
        
        TextComponent.text = LanguageConversion.Instance.WordConverter(Word);
    
    
    
    }




}
