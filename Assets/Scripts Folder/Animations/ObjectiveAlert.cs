using UnityEngine;
using System.Collections;

public class ObjectiveAlert : MonoBehaviour
{
    //THIS SCRIPT IS THE NOTIFICATION ALERT SYMBOL AT THE BEGINNING OF THE WORKPHASE//

    public Animator Anim, TimerAnim;
    
    public GameObject Objectives, Task1, EP, TimerText;

    public WorkPhaseTimer WPT;

    public bool TimerCheck = false;


    void Awake()
    {
        
        WPT = GameObject.Find("Timer Text").GetComponent<WorkPhaseTimer>();

        TimerAnim = GameObject.Find("Timer Text").GetComponent<Animator>();

        TimerAnim.enabled = false;


        TimerText.SetActive(false);


    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Objectives.SetActive(false);

        Task1.SetActive(false);

    
        Anim = GameObject.Find("Exclamation Points").GetComponent<Animator>(); 


        EP.SetActive(false);

       
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }


    //FUNCTION TO TRIGGER THE OBJECTIVE ANMIATION//
    public IEnumerator TriggerAnimation()
    {
        
        EP.SetActive(true);
       
        yield return new WaitForSeconds(0f);

        
        Anim.Play("AlertNotification");



        yield return new WaitForSeconds(2f);


        Anim.Play("SlidetoLeft");



        yield return new WaitForSeconds(2f);

        Anim.enabled = false;
        
        Objectives.SetActive(true);

        EP.SetActive(false);

        
        
        yield return new WaitForSeconds(1f);

        Task1.SetActive(true);


        yield return new WaitForSeconds(0.5f);

        TimerText.SetActive(true);

        TimerAnim.enabled = true;
       
        TimerAnim.Play("RevealTimer");

        TimerCheck = true;


        yield return new WaitForSeconds(4f);
       
        WPT.CanRunTimer = true;

    


     
        









    }




}
