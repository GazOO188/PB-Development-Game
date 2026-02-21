using UnityEngine;



//THIS CREATES A NEW MENU WHEN YOU RIGHT CLICK IN PORJECT FOLDER:

// RIGHTCLICK -> CREATE -> DIALOGUE -> CONVERSATION -> CREATES A NEW ASSET
[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/Conversation")]
public class DialogueData : ScriptableObject
{

    //THIS IS THE SPEAKER'S NAME//
    public string SpeakerName;

    //ALL THE POSSBLE DIALGOUES IN THE GAME//
    public string[] lines;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
