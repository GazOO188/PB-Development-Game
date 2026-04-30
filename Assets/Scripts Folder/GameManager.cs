using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool englishLanguage = true;
    public bool GameOver = false;

    public bool FinalTaskCompleted = false;


    public bool EndSequenceStarted = false;

    public bool inEnvelopeScene = false;
   
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // <-- persist across scenes
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // <-- remove duplicate
        }

    }

    void Update()
    {

        //Debug.Log(CanDisplayPhoneCallAgain);

    }


    public void ToggleLanguage()
    {
        // Switch language
        englishLanguage = !englishLanguage;
    }
}
