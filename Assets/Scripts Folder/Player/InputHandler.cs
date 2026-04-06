using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class InputHandler : MonoBehaviour
{
    // Source: https://www.youtube.com/watch?v=T2T82MWbbew
    public PlayerController player;

    public CollisionInteractions CI;

    public ObjectiveAlert OA;

    public WorkPhaseTimer WPT;

    [SerializeField] PlayerCamera playerCamera;

    [SerializeField] public DialogueData Dialogue, Resident1, Resident2, Resident3, Resident3A, Resident3B;

    [SerializeField] public GameObject EButton;


    private Coroutine typewriterCoroutine, ObjectiveCouroutine;


    DialogueData currentDialogue;

    [SerializeField] public int currentLine = 0;

    public bool isTalking = false;


    [Header("Bools")]
    public bool MetWithResidentOne = false;
    
    public bool MetWithResidentOneAgain = false;
    
    public bool MetWithResidentOneFinalTime = false;
   
    public bool MetWithResidentTwo = false;
   
    public bool MetWithResidentThree = false;
   
    public bool CanTriggerObjectiveAnimation = false;


    //BOOL FOR ENABLING/DISABLING MOVEMENT//
    public bool canMove = true;

    public bool canProgresstoNextDialogue = false;

    public InputAction _move, _look, _crouch, _interact, _WeatherStrip, _CaulkGun, _SprayFoam;

    void Awake()
    {
        _move = InputSystem.actions.FindAction("Move");
        _look = InputSystem.actions.FindAction("Look");
        _crouch = InputSystem.actions.FindAction("Crouch");
        _interact = InputSystem.actions.FindAction("Interact");

        _WeatherStrip = InputSystem.actions.FindAction("WeatherStrip");

        _CaulkGun = InputSystem.actions.FindAction("CaulkGun");

        _SprayFoam = InputSystem.actions.FindAction("SprayFoam");

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


        //OA = GameObject.Find("UI Manager").GetComponent<ObjectiveAlert>();


    }

    void Update()
    {
        if (!player.playerControl) return;

        canProgresstoNextDialogue = CI.LineFinished;
      
        if (canMove)
        {
            
        Vector2 movement = _move.ReadValue<Vector2>();
        player.Move(movement);

        }

        Vector2 look = _look.ReadValue<Vector2>();
        playerCamera.Rotate(look);

        if (_crouch.WasPressedThisFrame() && player.characterController.isGrounded)
            player.Crouch();




        //THIS IS FOR PRESSING E TO TALK//
       // return;
        if (_interact.WasPressedThisFrame())
        {

            if (!isTalking)
            {
                if (player.CanSeeBoss)
                {
                    displayDialouge(Dialogue);
                }



                // THIRD DIALOGUE (MOST ADVANCED)
               else if (player.ResidentOneSeen && WPT.TaskTwoCompleted)
               {               
                    displayDialouge(Resident3B);

                    MetWithResidentOne = true;
                    
                    MetWithResidentOneFinalTime = true;

                    canMove = false;
                    
                    WPT.CanRunTimer = false;
               }

               // SECOND DIALOGUE
               else if (player.ResidentOneSeen && WPT.TaskOneCompleted)
               {
                    displayDialouge(Resident3A);

                    MetWithResidentOne = true;
    
                    MetWithResidentOneAgain = true;

                    canMove = false;
    
                    WPT.CanRunTimer = false;
               }

                //FOR DISPLAYING THE FIRST DIALOGUE//
                else if (player.ResidentOneSeen)
                {
                    displayDialouge(Resident3);
                    MetWithResidentOne = true;
                    canMove = false;
                    WPT.CanRunTimer = false;
        
                }



             

              //  else if (player.ResidentTwoSeen)
               // {
             //       displayDialouge(Resident2);
                   // MetWithResidentTwo = true;
               // }

              //  else if (player.ResidentThreeSeen)
               // {
                 //   displayDialouge(Resident3);
                 //   MetWithResidentThree = true;
               // }




            }

            // CONTINUE TALKING//
            else if (canProgresstoNextDialogue)
            {
                NextLine();

            }
            
  





        }



        //DISPLAYS THE E BUTTON//
        ShowEButton();
    }

    //DISPLAYS THE DIALOGUE PHYSICALLY//
    public void displayDialouge(DialogueData data)
    {

        currentDialogue = data;

        currentLine = 0;

        isTalking = true;

        CI.DialogueText.enabled = true;

        CI.DialgouePanel.enabled = true;

        CI.WhoIsSpeakingTab.SetActive(true);

        ShowCurrentLine();

        TemporarilyDisableRaycast();



    }


    //SHOWS THE CURRENT LINE//
    void ShowCurrentLine()
    {
        if (typewriterCoroutine != null)
            StopCoroutine(typewriterCoroutine);

        string line = LanguageConversion.Instance.WordConverter(currentDialogue.lines[currentLine]);

        typewriterCoroutine = StartCoroutine(CI.ShowDialgoueText(line));
    }


    //FUNCTION THAT CONTROLS DISPLAYING MULTIPLE LINES OF DATA//
    void NextLine()
    {
        //INCREMENT THE CURRENTLINE VARIABLE//
        currentLine++;

        //IF THERES MORE DIALOGUE TO SAY, SHOW THE LINE//
        if (currentLine < currentDialogue.lines.Length)
        {
            ShowCurrentLine();

        }

        //IF NO MORE DIALOGUE LEFT, STOP THE CONVERSATION//
        else
        {
            EndDialogue();

            canMove = true;

            if (OA.TimerCheck)
            {
            
                WPT.CanRunTimer = true;


            }

         



            //START THE OBJECTIVE TASK ANIMATION IF MET WITH EITHER NPC//


            if (!CanTriggerObjectiveAnimation && (MetWithResidentOne || MetWithResidentTwo || MetWithResidentThree))
            {
                StartCoroutine(OA.TriggerAnimation());
                CanTriggerObjectiveAnimation = true;

                // Reset NPC flags so they don't interfere
                MetWithResidentOne = false;
                MetWithResidentTwo = false;
                MetWithResidentThree = false;
            }
        }
    }


    //THIS FUNCTION ENDS THE DIALOGUE ONCE THE PERSON HAS NO MRE DIALOGUE IN THEIR DATA//
    void EndDialogue()
    {
        //NO LONGER TALKING OR HAVE ANY MORE DIALOGUE TO SAY//
        isTalking = false;

        //HIDES DIALOGUE BOX//
        CI.DialogueText.enabled = false;
        CI.DialgouePanel.enabled = false;
        CI.WhoIsSpeakingTab.SetActive(false);

        //PLAYER CAN CAST RAYCAST TO DETECT THINGS/
        player.CanCast = true;



    }


    //THIS FUNCTION TEMPORARILY DISABLES RAYCAST//
    private void TemporarilyDisableRaycast()
    {


        player.CanCast = false;

        CI.InteractText.enabled = false;



    }




    //FUNCTION TO DISPLAY THE EBUTTON PROMPT//


    public void ShowEButton()
    {

        //SETS IT TO TRUE IF THE LINE HAS FINISHED//
        if (isTalking && (MetWithResidentOne || MetWithResidentThree || MetWithResidentTwo))
        {

            EButton.SetActive(CI.LineFinished);



        }


        else
        {


            if (!isTalking)
            {


                EButton.SetActive(false);







            }






        }



    }




}
