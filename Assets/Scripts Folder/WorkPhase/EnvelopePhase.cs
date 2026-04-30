using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class EnvelopePhase : MonoBehaviour
{
    
    //THIS SCRIPT IS FOR THE ENVELOPE PHASE//

    [Header("Bool")]
    [SerializeField] public bool DisplayEnvelopeIssues;
    
    [SerializeField] public bool DisplayEnvelopeIssues2 = false;
   
    [SerializeField] public bool DisplayEnvelopeIssues3 = false;
    
    [SerializeField] public bool TimerCheck = false;



    [Header("Envelope TaskStates")]
    [SerializeField] public bool EnvelopeTask1Completed;
    [SerializeField] public bool EnvelopeTask2Completed;
    [SerializeField] public bool EnvelopeTask3Completed;
    


    [Header("Scripts")]
    [SerializeField] private InputHandler IH;
   
    [SerializeField] public WorkPhaseTimer WPT;
    
    //WEATHERSTRIP//
    [SerializeField] public TrackWS TWS;

    //SPRAYFOAM//
    [SerializeField] public CheckForFoam CFF;

    //CAULK GUN//
    [SerializeField] public CrackEdges CE;
    


    [Header("Animation")]
    [SerializeField] public Animator Anim, TimerAnim;



    [Header("GameObjects")]
    [SerializeField] public GameObject Objectives, EP, TimerText;
    



    //TEXT//
    [Header("TextMeshPro")]
    [SerializeField] public TextMeshProUGUI TaskOneText;
    [SerializeField] public TextMeshProUGUI TaskTwoText;
    [SerializeField] public TextMeshProUGUI TaskThreeText;


    
    [Header("CheckMarks")] 
    
    [SerializeField] public GameObject CheckMarkForTask1;
    
    [SerializeField] public GameObject CheckMarkForTask2;
    
    [SerializeField] public GameObject CheckMarkForTask3;




    [Header("Tasks")] 
    
    [SerializeField] public GameObject Task1;
    
    [SerializeField] public GameObject Task2;
    
    [SerializeField] public GameObject Task3;



     //ANIMATION SECTION//
    [Header("Animation Settings")]
    
    [SerializeField] public Animator CheckAnim;

    [SerializeField] public Animator CheckAnim2;

    [SerializeField] public Animator CheckAnim3;

   
    //FOR THE 2ND AND 3RD ENVELOPE TASK//
    [SerializeField] public Animator Task2Anim;

    [SerializeField] public Animator FinalTaskAnim;




    [Header("Exclamation Points")]
    [SerializeField] public GameObject ExclamationPoint;
    [SerializeField] public GameObject ExclamationPoint2;
    [SerializeField] public GameObject ExclamtionPoint3;







    public bool CompletedTaskOne = true, noreset = false;

   
    private bool envelopeTask2Triggered = false;

    

    //FOR TRACKING TASKS COMPLETED//
    public int TaskComp = 0;
    
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(IH.MetWithResidentOneInEnvelopeScene && !IH.isTalking && !DisplayEnvelopeIssues2)
        {
            
            DisplayEnvelopeIssues2 = true;
        
            StartCoroutine(ShowTheNextTaskInList(Task2, CheckMarkForTask1, TaskOneText, TaskTwoText, "Investigate the door", Task2Anim, "ShowTask2"));

        }



        //THIS IS FOR COMPLETING THE WEATHERSTRIP TASK//

        if (TWS.complete && !EnvelopeTask1Completed && !noreset)
        {
            

        
         StartCoroutine(MarkObjectiveAsComplete(CheckMarkForTask1, ExclamationPoint, CheckAnim, TaskOneText, WPT.CanRunTimer, "Investigate the Window", "MarkTask"));
        
         TaskComp++;
        
         EnvelopeTask1Completed = true;
         
        
        }




        //THIS IS FOR COMPLETING THE SPRAY FOAM TASK//

        if(CFF.complete && !EnvelopeTask2Completed && EnvelopeTask1Completed && !envelopeTask2Triggered)
        {
            
            envelopeTask2Triggered = true;
            noreset = true;
            EnvelopeTask2Completed = true;

            TaskComp++;

            StartCoroutine(MarkObjectiveAsComplete(CheckMarkForTask2, ExclamationPoint2, CheckAnim2, TaskTwoText, WPT.CanRunTimer, "Investigate the door", "Task3Check"));

        }






        //THIS IS FOR SHOWING THE FINAL TASK -> CAULK GUN//


        if(CE.Fullysealed && !EnvelopeTask3Completed)
        {
            
            StartCoroutine(MarkObjectiveAsComplete(CheckMarkForTask3, ExclamtionPoint3, FinalTaskAnim, TaskThreeText, WPT.CanRunTimer, "Seal the crack", "MarkTask2"));

            EnvelopeTask3Completed = true;


        }


       
        
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


      // CUSTOM COROUTINE FOR STARTING THE NEXT OBJECTIVE//
    public IEnumerator MarkObjectiveAsComplete(GameObject Checkmark, GameObject EP, Animator CheckAnimator, TextMeshProUGUI TaskText, bool timerBool, string TaskDescription, string Animation)
    {

        yield return new WaitForSeconds(0f);

        //THIS CROSSES OUT THE TASK TO MARK IT COMPLETE//

        EP.SetActive(false);

       
        TaskText.GetComponent<KeyAnalyzer>().Word = TaskDescription;
       
        TaskText.GetComponent<KeyAnalyzer>().WordConversion();

        TaskText.text = "<s>" + TaskDescription + "</s>";



        Checkmark.SetActive(true);


        CheckAnimator.enabled = true;

        CheckAnimator.Play(Animation);
        
       
        timerBool = false;

   

        yield return new WaitForSeconds(2.3f);

        Checkmark.SetActive(false);

        KeyAnalyzer ka = TaskText.GetComponent<KeyAnalyzer>();

        Debug.Log("Setting new objective: Report to the Resident");

        ka.Word = "Report to the Resident";

        ka.WordConversion();

        TaskText.text = ka.Word;
        

    }


    
    
    //FUNCTION FOR SHOWING NEXT TASK IN THE LIST//
   public IEnumerator ShowTheNextTaskInList(GameObject NextTask, GameObject checkmark, TextMeshProUGUI PreviousText, TextMeshProUGUI NewText, string NextObjectiveText, Animator NextTaskAnim, string AnimationClip)
  {
    yield return new WaitForSeconds(0f);

    // SHOW NEXT TASK
    NextTask.SetActive(true);
    NextTaskAnim.enabled = true;
    NextTaskAnim.Play(AnimationClip);

    // CROSS OUT PREVIOUS TASK
    KeyAnalyzer kaPrev = PreviousText.GetComponent<KeyAnalyzer>();

    kaPrev.CanOverWrite = true;

    PreviousText.text = "<s>" + PreviousText.text + "</s>";

    checkmark.SetActive(true);

    // SET NEW TASK TEXT
    KeyAnalyzer kaNew = NewText.GetComponent<KeyAnalyzer>();

    kaNew.CanOverWrite = false; 

    kaNew.Word = NextObjectiveText;
    kaNew.WordConversion();

    yield return new WaitForSeconds(0f);
}


public void MarkFinalTask()
 {
        

  StartCoroutine(ShowTheNextTaskInList(Task3, CheckMarkForTask2, TaskTwoText, TaskThreeText, "Seal the crack", FinalTaskAnim, "FinalTask"));



}



    //CUSTOM COROUTINE FOR FINISHING THE LAST ENVELOPE TASK//
   
}