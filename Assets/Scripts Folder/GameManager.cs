using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool englishLanguage = true;
   
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

    }


    public void ToggleLanguage()
    {
        // Switch language
        englishLanguage = !englishLanguage;
    }
}
