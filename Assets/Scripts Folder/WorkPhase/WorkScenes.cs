using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WorkScenes : MonoBehaviour
{
    //THIS SCRIPT CONTROLS THE WORKPHASES//

    //TRACK IF ALL CONDITIONS ARE MET//

    public int TasksCompleted = 0;


    [SerializeField] public WorkPhaseTimer WPT;

    public bool CanReload = false;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //THIS LOADS SCENE BACK SCENE ONE//
        
        if(!CanReload && WPT.TimerforWorkPhase <= 0)
        {
            
            ReloadWorkPhaseOne();

            WPT.TimerforWorkPhase = 0;

            WPT.DisplayTimeronScreen(WPT.TimerforWorkPhase);

            CanReload = true;








        }


    }


    //LOAD WORKPHASE 1 SCENE//

    //SUCCESSFUL CONDITION//
    public void LoadWorkPhase()
    {
        
        if(TasksCompleted >= 3)
        {
            
        SceneManager.LoadScene("Round One Scene");

        }
       


    }



    //UNSUCCESSFUL CONDITION//

    public void ReloadWorkPhaseOne()
    {
        
    //GET THE CURRENT SCENE//
    Scene currentScene = SceneManager.GetActiveScene();
  
  
    //RELOAD IT BY ITS NAME//
    SceneManager.LoadScene(currentScene.name);



    }






}
