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
    [SerializeField] GameObject endGameButtons;
    [SerializeField] TextMeshProUGUI endText;

    [SerializeField] GameObject DialoguePanel;
    [SerializeField] GameObject SpeakerTab;
    [SerializeField] GameObject FadeOut;

    [SerializeField] public PlayableDirector EnvelopeDirector;
    [SerializeField] bool HasEndedElectrical = false;
    bool canLoad = true;


    [Header("GameObject")]
    [SerializeField] public List<GameObject> ObjectstoTurnOff = new List<GameObject>();




    void Start()
    {
        

   //HasEndedElectrical = false;

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


        //DISPLAY THANK YOU FOR PLAYING TEXT//

        if (EP.EnvelopeTask3Completed && GameManager.Instance.FinalTaskCompleted)
        {
            
            
            endText.text = LanguageConversion.Instance.WordConverter("Thank you for playing!");
 
            Cursor.visible = true;
            
            Cursor.lockState = CursorLockMode.None;
            
            GameManager.Instance.GameOver = true;

            endGameButtons.SetActive(true);


            TurnOffGameObjects();

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

    public IEnumerator DisplayWellDoneText()
    {
        yield return new WaitForSeconds(1.3f);


        DialoguePanel.SetActive(true);
        SpeakerTab.SetActive(true);

        IH.displayDialouge(IH.BossRoundOneEnd);

        
        
    }


    //FUNCTION TO TURN OFF ALL GAMEOBJECTS IN SCENE WHEN GAME OVER HAPPENS//


    public void TurnOffGameObjects()
    {
        
        foreach(GameObject Obj in ObjectstoTurnOff)
        {
            
            Obj.SetActive(false);


        }





    }
}