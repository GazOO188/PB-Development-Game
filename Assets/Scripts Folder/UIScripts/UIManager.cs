using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        //GAME UNFREEZES//
        Time.timeScale = 1f; 
        SceneLoader.Instance.LoadNextScene();

        //THE PLAYER SHOULD NOT BE IN THE ENVELOPE SCENE AND THE FINALTASK OF THE ELECTRIC IS NOT COMPLETED//
        GameManager.Instance.FinalTaskCompleted = false;

        GameManager.Instance.inEnvelopeScene = false;

    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
