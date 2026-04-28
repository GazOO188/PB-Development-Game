using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class WinLoseCondition : MonoBehaviour
{
    [SerializeField] WorkPhaseTimer timer;
    [SerializeField] InputHandler IH;
    [SerializeField] GameObject endGameButtons;
    [SerializeField] TextMeshProUGUI endText;
    [SerializeField] GameObject DialoguePanel;
    [SerializeField] GameObject SpeakerTab;
    [SerializeField] GameObject FadeOut;
    [SerializeField] public PlayableDirector EnvelopeDirector;
    [SerializeField] bool HasEndedElectrical = false;
    bool canLoad = true;


    void Start()
    {
        

   //HasEndedElectrical = false;

    }

    void Update()
    {
        if ((timer.TimerforWorkPhase == 0f) && !endGameButtons.activeInHierarchy)
        {
            endGameButtons.SetActive(true);
            endText.text = GameManager.Instance.FinalTaskCompleted?LanguageConversion.Instance.WordConverter("Thank you for playing!"):LanguageConversion.Instance.WordConverter("Time's up!");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameManager.Instance.GameOver = true;
        }

        // FIXED PART ONLY
      
    if (!GameManager.Instance.EndSequenceStarted && GameManager.Instance.FinalTaskCompleted)
    {
        GameManager.Instance.EndSequenceStarted = true;

        endText.text = GameManager.Instance.FinalTaskCompleted
            ? LanguageConversion.Instance.WordConverter("Thank you for playing!")
            : LanguageConversion.Instance.WordConverter("Time's up!");

        //StartCoroutine(LoadSceneAfterDelay());

        StartCoroutine(DisplayWellDoneText());


        Debug.Log("End sequence triggered");
    }

    }

   
    public IEnumerator LoadSceneAfterSomeDelay()
    {
        //FadeOut.SetActive(true);
        EnvelopeDirector.Play();
        yield return new WaitForSeconds(2f);
       
        LoadEnvelopeScene();

    }

    public void ReturnToMain()
    {
        GameManager.Instance.GameOver = false;
        SceneLoader.Instance.LoadScene("Title");
    }

    public void RestartLevel()
    {
        GameManager.Instance.GameOver = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneLoader.Instance.ReloadCurrentScene();
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

        GameManager.Instance.CanDisplayPhoneCallAgain = false;
        DialoguePanel.SetActive(true);
        SpeakerTab.SetActive(true);

        IH.displayDialouge(IH.BossRoundOneEnd);

        
        
    }
}