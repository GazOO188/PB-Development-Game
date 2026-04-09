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
    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI TaskOneText;
    public TextMeshProUGUI TaskTwoText;
    public TextMeshProUGUI TaskThreeText;

    //BOOLS//
    [Header("Bools")]
    public bool CanRunTimer = true;
    public bool TaskOneCompleted = false;
    public bool DisplaySecondTask = false;
    public bool TaskTwoCompleted = false;
    public bool DisplayFinalTask = false;
    public bool FinalTaskCompleted = false;


    //GAMEOBJECT REFERENCES//
    [Header("GameObjects")]
    public GameObject ExclamationPoint;
    public GameObject ExclamationPoint2;
    public GameObject ExclamtionPoint3;
    public GameObject CheckMark;
    public GameObject CheckMarkForTask2;
    public GameObject CheckMarkForTask3;
    public GameObject Task2;
    public GameObject Task3;


    //ANIMATION SECTION//
    [Header("Animation Settings")]
    public Animator CheckAnim;

    public Animator CheckAnim2;

    public Animator CheckAnim3;

    //THIS SHOWS THE SECOND OBJECTIVE "FIX THE FAULTY BREAKER"//
    public Animator Task2Anim;

    public Animator FinalTaskAnim;




    //SCRIPT REFERENCES//
    [Header("Script References")]
    public InputHandler IH;
    public CircuitBreaker CB;
    public Outlet outlet;


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



            }


            else
            {
                //IF TIMER GOES BELOW OR EQUALS TO 0, CAP THE TIMER TO 0//

                if (TimerforWorkPhase <= 0)
                {
                    //SET TIMER TO BE 0, CAP TIMER//
                    TimerforWorkPhase = 0;

                    CanRunTimer = false;



                }



            }



        }

        //START THE COROUTINE TO SHOW NEXT OBJECTIVE//
        if (!TaskOneCompleted)
        {

            StartCoroutine(ShowNextObjective());

        }


        //STARTS THE COROUTINE FOR SHOWING NEXT OBJECTIVE AFTER OBJ 2//


        if (!TaskTwoCompleted)
        {

            StartCoroutine(MarkTask2());


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


        }




        //CHECKS OFF THE FINAL TASK//
        if (!FinalTaskCompleted)
        {

            StartCoroutine(MarkTask3());


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
        if (outlet.complete && !TaskOneCompleted)
        {

            ExclamationPoint.SetActive(false);

            TaskOneText.text = "<s> Repair the Broken Outlet </s>";



            CheckMark.SetActive(true);


            CheckAnim.enabled = true;

            CheckAnim.Play("MarkTask");

            CanRunTimer = false;



            yield return new WaitForSeconds(2.3f);

            CheckMark.SetActive(false);

            TaskOneText.text = "Report to the Resident";

            CanRunTimer = true;


            TaskOneCompleted = true;

        }



    }



    //COROTUINE FOR SHOWING THE SECOND OBJECTIVE ON SCREEN//


    public IEnumerator ShowTheSecondTask()
    {

        yield return new WaitForSeconds(0f);

        Task2.SetActive(true);

        Task2Anim.enabled = true;

        Task2Anim.Play("ShowTask2");

        TaskOneText.text = "<s> Report to the Resident </s>";





    }


    //MARK THE SECOND TASK AS DONE//

    public IEnumerator MarkTask2()
    {

        yield return new WaitForSeconds(0f);


        //FOR CIRCUIT BREAKER TASK//
        if (CB.doublePanelComplete && !TaskTwoCompleted)
        {

            ExclamationPoint2.SetActive(false);

            TaskTwoText.text = "<s> Fix the faulty breaker </s>";



            CheckMarkForTask2.SetActive(true);


            CheckAnim2.enabled = true;

            CheckAnim2.Play("Mark2");

            CanRunTimer = false;



            yield return new WaitForSeconds(2.3f);

            CheckMarkForTask2.SetActive(false);

            TaskTwoText.text = "Report to the Resident";

            CanRunTimer = true;




            TaskTwoCompleted = true;
        }



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
        TaskTwoText.text = "<s> Report to the Resident </s>";








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
            TaskThreeText.text = "<s> Restore Bedroom power <s>";


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
            TaskThreeText.text = "";
          
            // THANK YOU, JULS
            // MARK TASK AS COMPLETE//
            FinalTaskCompleted = true;
        }



    }









}

