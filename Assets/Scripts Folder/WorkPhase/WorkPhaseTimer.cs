using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorkPhaseTimer : MonoBehaviour
{
    //THIS SCRIPT IS FOR THE TIMER//

    public float TimerforWorkPhase = 180f; // 3 MINUTES//

    public Text TimerText;

    public bool CanRunTimer = true;

    
    
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



        //FUNCTION TO DISPLAY THE TIMER//

        void DisplayTimeronScreen(float time)
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





    }
}
