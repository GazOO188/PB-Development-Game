using UnityEngine;
using TMPro;

public class WinLoseCondition : MonoBehaviour
{
    [SerializeField] WorkPhaseTimer timer;
    [SerializeField] GameObject endGameButtons;
    [SerializeField] TextMeshProUGUI endText;

    void Update()
    {
        if ((timer.TimerforWorkPhase == 0f || timer.FinalTaskCompleted) && !endGameButtons.activeInHierarchy)
        {
            endGameButtons.SetActive(true);
            endText.text = timer.FinalTaskCompleted ? "Thank you for playing!" : "Time's up!";
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameManager.Instance.GameOver = true;
        }
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
}
