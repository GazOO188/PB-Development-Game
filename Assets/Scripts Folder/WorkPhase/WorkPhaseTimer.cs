using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class WorkPhaseTimer : MonoBehaviour
{
    //THIS SCRIPT IS FOR THE TIMER//

    //TIMER VARIABLE//
    [Header("Timer")]
    public float TimerforWorkPhase = 360f; // 6 MINUTES//

    //TEXT//
    [Header("TextMeshPro")]
    [SerializeField] public TextMeshProUGUI TimerText;
    [SerializeField] public TextMeshProUGUI TaskOneText;
    [SerializeField] public TextMeshProUGUI TaskTwoText;
    [SerializeField] public TextMeshProUGUI TaskThreeText;

    //BOOLS//
    [Header("Bools")]
    public bool CanRunTimer = true;
    public bool TaskOneDisplayed = false;
    [SerializeField] public bool TaskOneCompleted = false;
    [SerializeField] public bool DisplaySecondTask = false;
    [SerializeField] public bool TaskTwoCompleted = false;
    [SerializeField] public bool DisplayFinalTask = false;
    [SerializeField] public bool FinalTaskCompleted = false;
    [SerializeField] public bool CanMarkTask1 = false;
    [SerializeField] public bool CanMarkTask2 = false;
    [SerializeField] public bool CanMarkTask3 = false;
    [SerializeField] public bool PlayerPressedI = false;
    [SerializeField] public bool CanHideInventoryText = false;
    [SerializeField] public bool CanHideSpaceText = false;
    [SerializeField] public bool CanShowOnce= false;


    //GAMEOBJECT REFERENCES//
    [Header("GameObjects")]
    [SerializeField] public GameObject ExclamationPoint;
    [SerializeField] public GameObject ExclamationPoint2;
    [SerializeField] public GameObject ExclamtionPoint3;
    [SerializeField] public GameObject CheckMark;
    [SerializeField] public GameObject CheckMarkForTask2;
    [SerializeField] public GameObject CheckMarkForTask3;
    [SerializeField] public GameObject Task1;
    [SerializeField] public GameObject Task2;
    [SerializeField] public GameObject Task3;
    [SerializeField] public GameObject ObjectiveText;
    [SerializeField] public GameObject Timer;
    [SerializeField] public GameObject HelperText;
    [SerializeField] public GameObject InventoryHelperText;
    [SerializeField] public GameObject PressSpacetoCycleText;


    //ANIMATION SECTION//
    [Header("Animation Settings")]
    [SerializeField] public Animator CheckAnim;

    [SerializeField] public Animator CheckAnim2;

    [SerializeField] public Animator CheckAnim3;

    //THIS SHOWS THE SECOND OBJECTIVE "FIX THE FAULTY BREAKER"//
    [SerializeField] public Animator Task2Anim;

    [SerializeField] public Animator FinalTaskAnim;




    //SCRIPT REFERENCES//
    [Header("Script References")]
    public InputHandler IH;
    public CircuitBreaker CB;
    public Outlet outlet;
    public ObjectiveAlert OA;


    void Awake()
    {

        outlet = GameObject.Find("Outlet Manager").GetComponent<Outlet>();

        //DISABLE THE CHECK ANIMATIONS FROM HAPPENING//
        CheckAnim.enabled = false;

        CheckAnim2.enabled = false;

        CheckAnim3.enabled = false;


        //DISABLE THE TASKS FROM SHOWING//
        Task2Anim.enabled = false;

        Task2.SetActive(false);

        Task3.SetActive(false);


        







    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {


        //CODE TO MAKE THE TIMER COUNTDOWN//

        if (CanRunTimer)
        {

            if (TimerforWorkPhase > 0)
            {
                //MAKE THE TIMER GO DOWN//
                TimerforWorkPhase -= Time.deltaTime;

                //WHILE COUNTING DOWN UPDATE THE TIMER ON SCREEN//
                DisplayTimeronScreen(TimerforWorkPhase);

                if (!TaskOneDisplayed) TaskOneDisplayed = true;



            }


            else
            {
                //IF TIMER GOES BELOW OR EQUALS TO 0, CAP THE TIMER TO 0//

                if (TimerforWorkPhase <= 0)
                {
                    //SET TIMER TO BE 0, CAP TIMER//
                    TimerforWorkPhase = 0;

                    //DISPLAY THE TIMER CORRECTLY//

                    DisplayTimeronScreen(TimerforWorkPhase);

                    CanRunTimer = false;
                }



            }



        }

        //START THE COROUTINE TO SHOW NEXT OBJECTIVE//
        if (!TaskOneCompleted && !CanMarkTask1 && outlet.complete)
        {

            StartCoroutine(ShowNextObjective());

            CanMarkTask1 = true;

        }


        //STARTS THE COROUTINE FOR SHOWING NEXT OBJECTIVE AFTER OBJ 2//


        if (!TaskTwoCompleted && !CanMarkTask2 && CB.doublePanelComplete)
        {

            StartCoroutine(MarkTask2());

            CanMarkTask2 = true;


        }



        //FOR DISPLAYING THE SECOND OBJECTIVE ON SCREEN//
        if (IH.MetWithResidentOneAgain && !IH.isTalking && !DisplaySecondTask)
        {


            StartCoroutine(ShowTheSecondTask());

            DisplaySecondTask = true;



        }


        //FOR DISPLAYING THE THIRD OBJECTIVE ON SCREEN//


        if (IH.MetWithResidentOneFinalTime && !IH.isTalking && !DisplayFinalTask)
        {


            StartCoroutine(ShowFinalTask());

            DisplayFinalTask = true;


        }




        //CHECKS OFF THE FINAL TASK//
        if (!FinalTaskCompleted && !CanMarkTask3 && CB.singlePanelComplete)
        {

            StartCoroutine(MarkTask3());

            CanMarkTask3 = true;


        }



  

        //LOGIC FOR HIDING TEXT//

        if(Input.GetKeyDown(KeyCode.I) && !CanHideInventoryText)
        {
            

            HideInventory();

            CanHideInventoryText = true;

            



        }


        //LOGIC FOR HIDING SPACE TEXT//

        if(Input.GetKeyDown(KeyCode.Space) && !CanHideSpaceText)
        {
            

          PressSpacetoCycleText.SetActive(false);

          CanHideSpaceText = true;
          

    



        }


        //FOR SHOWING INVENTORY PROMPT//

        if (!CanShowOnce)
        {
            
        
        ShowInventory();

        CanShowOnce = true;

        }

    }



    //FUNCTION TO DISPLAY THE TIMER//

    public void DisplayTimeronScreen(float time)
    {


        //DIVIDE TIME BY 60 TO GET HOW MUCH MINUTES FIT INTO THE TIME:

        //EX: 120 SECONDS -> 2 MINUTES -> 120/60 = 2 MINUTES//

        //USE FLOORTOINT TO GET WHOLE MINUTES//
        float minutes = Mathf.FloorToInt(time / 60);


        //MODULO GIVES REMAINDER AFTER DIVISION//

        //THIS GIVES THE REMAINING SECONDS AFTER DIVISION//
        float seconds = Mathf.FloorToInt(time % 60);


        //FORMAT THE TIMERTEXT TO BE IN MINUTES TO SECONDS//
        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);


    }


    //COROUTINE FOR STARTING THE NEXT OBJECTIVE//
    public IEnumerator ShowNextObjective()
    {

        yield return new WaitForSeconds(0f);

        //THIS CROSSES OUT THE TASK TO MARK IT COMPLETE//

        ExclamationPoint.SetActive(false);

 
        TaskOneText.GetComponent<KeyAnalyzer>().Word = "Repair the Broken Outlet";
       
        TaskOneText.GetComponent<KeyAnalyzer>().WordConversion();

        TaskOneText.text = "<s>" + TaskOneText.text + "</s>";



        CheckMark.SetActive(true);


        CheckAnim.enabled = true;

        CheckAnim.Play("MarkTask");

        CanRunTimer = false;



        yield return new WaitForSeconds(2.3f);

        CheckMark.SetActive(false);
        
        TaskOneText.GetComponent<KeyAnalyzer>().CanOverWrite = false;

        TaskOneText.GetComponent<KeyAnalyzer>().Word = "Report to the Resident";

        TaskOneText.GetComponent<KeyAnalyzer>().WordConversion();
       
       
        CanRunTimer = true;


        TaskOneCompleted = true;

    }







    //COROTUINE FOR SHOWING THE SECOND OBJECTIVE ON SCREEN//


    public IEnumerator ShowTheSecondTask()
    {

        yield return new WaitForSeconds(0f);

        Task2.SetActive(true);

        Task2Anim.enabled = true;

        Task2Anim.Play("ShowTask2");

        
        TaskOneText.GetComponent<KeyAnalyzer>().Word = "Report to the Resident";
       
        TaskOneText.GetComponent<KeyAnalyzer>().WordConversion();

        TaskOneText.GetComponent<KeyAnalyzer>().CanOverWrite = true;

        TaskOneText.text = "<s>" + TaskOneText.text + "</s>";

        CheckMark.SetActive(true);



    }


    //MARK THE SECOND TASK AS DONE//

    public IEnumerator MarkTask2()
    {

        yield return new WaitForSeconds(0f);



        ExclamationPoint2.SetActive(false);

        TaskTwoText.text = "<s> Fix the faulty breaker </s>";



        CheckMarkForTask2.SetActive(true);


        CheckAnim2.enabled = true;

        CheckAnim2.Play("Mark2");

        CanRunTimer = false;



        yield return new WaitForSeconds(2.3f);

        CheckMarkForTask2.SetActive(false);

        
        TaskTwoText.GetComponent<KeyAnalyzer>().Word = "Report to the Resident";
       
        TaskTwoText.GetComponent<KeyAnalyzer>().WordConversion();

       


        CanRunTimer = true;


        TaskTwoCompleted = true;


    }

    //FOR PRESSING I//
    
    public void ShowInventory()
    {

        if (OA.TimerCheck)
        {
            
         //TURN ON INVENTORY TEXT//
        InventoryHelperText.SetActive(true);  


        }


    }

    //HIDE THE INVENTORY PROMPT//
    public void HideInventory()
    {
        
        InventoryHelperText.SetActive(false);

        ShowSpacePrompt();

    }


    //FOR PRESSING SPACE WHILE IN INVENTORY (PROMPT)//


    public void ShowSpacePrompt()
    {
        
        PressSpacetoCycleText.SetActive(true);


    }





    //THIS FUNCTION SHOWS THE LAST TASK//

    public IEnumerator ShowFinalTask()
    {

        yield return new WaitForSeconds(0f);

        Task3.SetActive(true);

        FinalTaskAnim.enabled = true;

        //SHOWS THE LAST TASK//
        FinalTaskAnim.Play("FinalTask");

        //CROSS OUT THE TASK TWO TASK//
            
        TaskTwoText.GetComponent<KeyAnalyzer>().Word = "Report to the Resident";
       
        TaskTwoText.GetComponent<KeyAnalyzer>().WordConversion();

        TaskTwoText.GetComponent<KeyAnalyzer>().CanOverWrite = true;

        TaskTwoText.text = "<s>" + TaskTwoText.text + "</s>";

        CheckMarkForTask2.SetActive(true);








    }


    

    //FOR MARKING THE FINAL TASK//

    public IEnumerator MarkTask3()
    {

        yield return new WaitForSeconds(0f);


        //FOR CIRCUIT BREAKER TASK//
        if (CB.singlePanelComplete && !FinalTaskCompleted)
        {
            ExclamtionPoint3.SetActive(false);


            //CROSS OUT THE TASK TEXT//
            TaskThreeText.GetComponent<KeyAnalyzer>().Word = "Repair the Broken Outlet";
       
            TaskThreeText.GetComponent<KeyAnalyzer>().WordConversion();

            TaskThreeText.text = "<s>" + TaskThreeText.text + "</s>";



            //ACTIVATE THE CHECKMARK 3 GAMEOBJECT//
            CheckMarkForTask3.SetActive(true);


            //ENABLE ANIMATOR//
            CheckAnim3.enabled = true;


            //PLAY ANIMATION//
            CheckAnim3.Play("Task3Check");


            //DISABLE TIMER//

            CanRunTimer = false;


            yield return new WaitForSeconds(2.3f);

            //HIDE/DISBALE CHECKMARK GAMEOBJECT//
            CheckMarkForTask3.SetActive(false);


            //EMPTY TEXT FOR NOW//

            TaskOneText.text = "";

            TaskTwoText.text = "";
           
            TaskThreeText.text = "";


            //EMPTY TASKS//

            Task1.SetActive(false);
            
            Task2.SetActive(false);

            Task3.SetActive(false);

            ObjectiveText.SetActive(false);

            Timer.SetActive(false);

            HelperText.SetActive(false);

            CheckMarkForTask2.SetActive(false);

            CheckMark.SetActive(false);


            // THANK YOU, JULS
            // MARK TASK AS COMPLETE//
            FinalTaskCompleted = true;
        }



    }









}

