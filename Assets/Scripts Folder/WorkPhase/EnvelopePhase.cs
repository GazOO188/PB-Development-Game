using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class EnvelopePhase : MonoBehaviour
{
    
    //THIS SCRIPT IS FOR THE ENVELOPE PHASE//

    [Header("Bool")]
    [SerializeField] public bool DisplayEnvelopeIssues;
    [SerializeField] public bool TimerCheck = false;



    [Header("Envelope TaskStates")]
    [SerializeField] public bool EnvelopeTask1Completed;
    [SerializeField] public bool EnvelopeTask2Completed;
    [SerializeField] public bool EnvelopeTask3Completed;
    


    [Header("Scripts")]
    [SerializeField] private InputHandler IH;
    [SerializeField] public WorkPhaseTimer WPT;


    [Header("Animation")]
    [SerializeField] public Animator Anim, TimerAnim;



    [Header("GameObjects")]
    [SerializeField] public GameObject Objectives, Task1, EP, TimerText;


       




    
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        
    }



    //FUNCTION TO TRIGGER THE OBJECTIVE ANMIATION//
    public IEnumerator TriggerAnimationForEnvelopePhase()
    {
        
        EP.SetActive(true);

        //WPT.CanRunTimer = false;
       
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

        //CHANGE TEXT TO SAY SUMTHIN DIFFERENT//

        Task1.GetComponentInChildren<KeyAnalyzer>().Word = "Investigate the Window";
        
        Task1.GetComponentInChildren<KeyAnalyzer>().WordConversion();


        yield return new WaitForSeconds(0.5f);

        TimerText.SetActive(true);

        TimerAnim.enabled = true;
       
        TimerAnim.Play("RevealTimer");

        TimerCheck = true;


        yield return new WaitForSeconds(4f);
       
        WPT.CanRunTimer = true;

   

    }



}
