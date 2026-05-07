using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class WinLoseCondition : MonoBehaviour
{
    [SerializeField] WorkPhaseTimer timer;
    [SerializeField] InputHandler IH;
    [SerializeField] EnvelopePhase EP;
    [SerializeField] PlayerController PC;
    
    
    [SerializeField] GameObject endGameButtons;
    [SerializeField] TextMeshProUGUI endText, TrueEndText;

    [SerializeField] GameObject DialoguePanel;
    [SerializeField] GameObject SpeakerTab;
    [SerializeField] GameObject FadeOut;
    

    [SerializeField] public PlayableDirector EnvelopeDirector;
    [SerializeField] bool HasEndedElectrical = false;
    bool canLoad = true;
    bool GameCompleted = false;


    [Header("GameObject")]
    [SerializeField] public List<GameObject> ObjectstoTurnOff = new List<GameObject>();

    
    [Header("Animator")]
    [SerializeField] public Animator EndingCreditsAnim;




    void Start()
    {
        

   //HasEndedElectrical = false;

   EndingCreditsAnim.enabled = false;

    }

    void Update()
    {
        //FOR DISPLAYING GAMEOVER TEXT, WHEN TIMER IS 0//
        if ((timer.TimerforWorkPhase == 0f) && !endGameButtons.activeInHierarchy || GameManager.Instance.inEnvelopeScene && (timer.TimerforWorkPhase == 0f))
        {
            endGameButtons.SetActive(true);
            endText.text = LanguageConversion.Instance.WordConverter("Time's up!");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameManager.Instance.GameOver = true;

            TurnOffGameObjects();
        }

      
       if (!GameManager.Instance.EndSequenceStarted && GameManager.Instance.FinalTaskCompleted)
       {
            GameManager.Instance.EndSequenceStarted = true;

            //StartCoroutine(LoadSceneAfterDelay());

            StartCoroutine(DisplayWellDoneText());


            Debug.Log("End sequence triggered");
       }


        //DISPLAY THANK YOU FOR PLAYING TEXT && END CREDIT SCENE////
        if (EP.EnvelopeTask3Completed && GameManager.Instance.FinalTaskCompleted && !GameCompleted)
        {
            
        
            TurnOffGameObjects();

            StartCoroutine(DisplayEndCredits());

            GameCompleted = true;



        }

    }

   
    public IEnumerator LoadSceneAfterSomeDelay()
    {
        //FadeOut.SetActive(true);
        EnvelopeDirector.Play();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        yield return new WaitForSeconds(2f);
       
        LoadEnvelopeScene();

        GameManager.Instance.inEnvelopeScene = true;

    }

    public void ReturnToMain()
    {
        GameManager.Instance.GameOver = false;
        SceneLoader.Instance.LoadScene("Title");
    }

    public void RestartLevel()
    {
        GameManager.Instance.GameOver = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneLoader.Instance.ReloadCurrentScene();

        //THE PLAYER SHOULD NOT BE IN THE ENVELOPE SCENE AND THE FINALTASK OF THE ELECTRIC IS NOT COMPLETED//
        GameManager.Instance.FinalTaskCompleted = false;

        GameManager.Instance.inEnvelopeScene = false;

       
    }

    public void LoadEnvelopeScene()
    {
    
    if (canLoad)
    {

        GameManager.Instance.inEnvelopeScene = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        canLoad = false;
    }
    
    }

    //THIS CORUITNE IS FOR DISPLAYING THE TEXT TO TRANSITION TO THE ENVELOPE SCENE//
    public IEnumerator DisplayWellDoneText()
    {
        yield return new WaitForSeconds(1.3f);

        IH.canMove = false;

        DialoguePanel.SetActive(true);
        
        SpeakerTab.SetActive(true);   
        
        
        IH.displayDialouge2(IH.BossRoundOneEnd);

        
        
    }


    //FUNCTION TO TURN OFF ALL GAMEOBJECTS IN SCENE WHEN GAME OVER HAPPENS//


    public void TurnOffGameObjects()
    {
        
        foreach(GameObject Obj in ObjectstoTurnOff)
        {
            
            Obj.SetActive(false);


        }


    }



    //FUNCTION TO DISPLAY END CREDITS//

    public IEnumerator DisplayEndCredits()
    {
        
            yield return new WaitForSeconds(2f);
            
            EnvelopeDirector.Play();

            PC.canSummonInventory = false;

            IH.canPause = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            yield return new WaitForSeconds(2f);

            EndingCreditsAnim.enabled = true;
            
            EndingCreditsAnim.Play("Ending");

        
            Cursor.visible = true;
            
            Cursor.lockState = CursorLockMode.None;
 


          


    
           // endText.text = LanguageConversion.Instance.WordConverter("Thank you for playing!");


         
           // Cursor.visible = true;
            
            //Cursor.lockState = CursorLockMode.None;
            
            //GameManager.Instance.GameOver = true;

            //endGameButtons.SetActive(true);




    }
}