using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class WorkPhaseTimer : MonoBehaviour
{
    //THIS SCRIPT IS FOR THE TIMER//

    public float TimerforWorkPhase = 360f; // 6 MINUTES//

    public TextMeshProUGUI TimerText, TaskOneText;

    public bool CanRunTimer = true, TaskOneCompleted = false, DisplaySecondTask = false;

    public Outlet outlet;

    [Header("GameObjects")]

    public GameObject ExclamationPoint, CheckMark, Task2;


   //ANIMATION SECTION//
   [Header("Animation Settings")]

   public Animator CheckAnim;

   public Animator Task2Anim;


   [Header("Script References")]

   public InputHandler IH;
 
    
    void Awake()
    {
       outlet = GameObject.Find("Outlet Manager").GetComponent<Outlet>();

       CheckAnim.enabled = false;

       Task2Anim.enabled = false;

       Task2.SetActive(false);


      
   
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
            
            if(TimerforWorkPhase > 0)
            {
                //MAKE THE TIMER GO DOWN//
                TimerforWorkPhase -= Time.deltaTime;

                //WHILE COUNTING DOWN UPDATE THE TIMER ON SCREEN//
                DisplayTimeronScreen(TimerforWorkPhase);
           
           
           
            }


            else
            {
                //IF TIMER GOES BELOW OR EQUALS TO 0, CAP THE TIMER TO 0//

                if(TimerforWorkPhase <= 0)
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



        //FOR DISPLAYING THE SECOND OBJECTIVE ON SCREEN//
        if(IH.MetWithResidentOneAgain && !IH.isTalking && !DisplaySecondTask)
        {
            
        
        StartCoroutine(ShowTheSecondTask());

        DisplaySecondTask = true;



        }

      
    }



        //FUNCTION TO DISPLAY THE TIMER//

        public void DisplayTimeronScreen(float time)
        {
            
        
            //DIVIDE TIME BY 60 TO GET HOW MUCH MINUTES FIT INTO THE TIME:

            //EX: 120 SECONDS -> 2 MINUTES -> 120/60 = 2 MINUTES//

            //USE FLOORTOINT TO GET WHOLE MINUTES//
            float minutes = Mathf.FloorToInt(time/60);


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




      




    }

