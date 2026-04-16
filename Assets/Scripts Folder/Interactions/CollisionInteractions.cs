using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class CollisionInteractions : MonoBehaviour
{
    //THIS SCRIPT IS FOR INTERACTING/COLLIDING WITH THE BOSS NPC//

    
    [Header("UI")]

    
    [SerializeField] public TextMeshProUGUI InteractText;

    [SerializeField] public TextMeshProUGUI DialogueText;

    [SerializeField] public Image DialgouePanel;

    [SerializeField] public GameObject WhoIsSpeakingTab;
 


    [Header("TypeEffect")]


    [SerializeField] public float typeSpeed = 1f;

    
    
    public bool LineFinished = false;



    //REFERENCE TO SCRIPTABLE DATA OBJECT FOR THE DIALOGUE SYSTEM
    public DialogueData Dialogue;

    public PlayerController PC;

    public InputHandler IH;


  
    




    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        InteractText.enabled = false;

        DialogueText.enabled = false;
        
        DialgouePanel.enabled = false;

        WhoIsSpeakingTab.SetActive(false);


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //FUNCTION TO SHOW THE TEXT WITH TYPE WRITIGNG EFFECT//
    public IEnumerator ShowDialgoueText(string TexttoDisplay)
    {

        //FRESH INSTANCE//
        LineFinished = false;

        //CLEAR TEXT//
        DialogueText.text = "";

        foreach(char Letter in TexttoDisplay)
        {
            
            DialogueText.text += Letter;

            yield return new WaitForSeconds(typeSpeed);

            
        
        
        }

        //THE LINE HAS FINISHED WRITING OUT//
         LineFinished = true;

      
      


      
      
    
    }


}
