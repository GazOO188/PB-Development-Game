using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UITrigger : MonoBehaviour
{
    //THIS SCRIPT IS TO MANAGE UI//

      

    [Header("UI Panel")]

    [SerializeField] public GameObject SettingsMenu;


    [Header("Pause Menu")]

    [SerializeField] public GameObject PauseMenu;

    
    [Header("How To Play Menu")]

    [SerializeField] public GameObject ControlsMenu;
    
    [SerializeField] public GameObject Tutorial;

    
    [Header("Script References")]

    [SerializeField] public InputHandler IH;
    
    [SerializeField] public PlayerController PC;


    
    [Header("Tutorial")]

    [SerializeField] public List<GameObject> Tutorials = new List<GameObject>();


    //ENUM TO SWITCH BETWEEN STATES


    public enum UIMenuStates
    {
        
        Resume,

        Pause,

        Settings,

        HowToPlay,



    }


    public UIMenuStates CurrentUIState;


   
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       


        //SET UI PANELS TO BE HIDDEN UNTIL CALLED//
        SettingsMenu.SetActive(false);

        
        PauseMenu.SetActive(false);




    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //THIS FUNCTION MANAGES UI STATES//

    public void SetUIState(UIMenuStates UIState)
    {
        
        CurrentUIState = UIState;



        switch (UIState)
        {
            //IF CURRENT STATE IS RESUME//
            case UIMenuStates.Resume:
            {
           
            //TURN OFF ALL PAUSE MENU BUTTONS//
            //DISABLE PAUSE TEXT//
            PauseMenu.SetActive(false);

             //ENABLE LOOKING AND MOVING AGAIN//
             IH.canLook = true;
             PC.playerControl = true;


             //LOCK AND HIDE THE CURSOR//
             Cursor.visible = false;
             Cursor.lockState = CursorLockMode.Locked;


             //UNFREEZE GAME//
             Time.timeScale = 1f;

             break;

            }

           
            case UIMenuStates.Settings:
            {
                    
            SettingsMenu.SetActive(true);

            break;


            }


            case UIMenuStates.Pause:
            {
                    

  
            //ACTIVATE PAUSEMENU//
            PauseMenu.SetActive(true);

            break;



            }



            case UIMenuStates.HowToPlay:
            {
                    
            
            ControlsMenu.SetActive(true);
            
            Tutorial.SetActive(false);
            
            
            break;



            }



        }





    }


    //FUNCTION FOR SETTINGS//
    public void OpenSettings()
    {
        

     SetUIState(UIMenuStates.Settings);


    }

    //FUNCTION TO OPEN CONTROLS MENU WITH THE CONTROLS AND HOW TO PLAY TAB//
    public void OpenControls()
    {
        
        //ACTIVATE HOW TO PLAY MENU//
        SetUIState(UIMenuStates.HowToPlay);



    }



    //FUNCTION TO OPEN HOW TO PLAY MENU//

    public void TutorialGuide()
    {
        
        //ACTIVATE HOW TO PLAY FROM TAB//

       Tutorial.SetActive(true);


       Tutorials[1].SetActive(false);

       Tutorials[2].SetActive(false);

       Tutorials[3].SetActive(false);



    }


    //FUNCTION TO OPEN TUTORIAL 1 MENU//

    public void OpenTutorialOne()
    {
        
        //ACTIVATE HOW TO PLAY FROM TAB//

        Tutorials[1].SetActive(true);



    }




    //FUNCTION TO GO BACK TO TUTORIAL 1 MENU//

    public void GoBacktoTutorialOne()
    {
        
        //ACTIVATE HOW TO PLAY FROM TAB//
        Tutorials[1].SetActive(false);

        //SET TUTORIAL ONE TO BE DEACTIVATED//
        Tutorials[0].SetActive(true);



    }




    //FUNCTION TO OPEN TUTORIAL 2 MENU//

    public void GoBacktoTutorialTwo()
    {
        
        //ACTIVATE HOW TO PLAY FROM TAB//
        Tutorials[1].SetActive(true);

        //SET TUTORIAL ONE TO BE DEACTIVATED//
        Tutorials[2].SetActive(false);



    }




    //FUNCTION TO OPEN TUTORIAL 1 MENU//

    public void OpenTutorialTwo()
    {
        
        //ACTIVATE HOW TO PLAY FROM TAB//
        Tutorials[2].SetActive(true);

        //SET TUTORIAL ONE TO BE DEACTIVATED//
        Tutorials[1].SetActive(false);

        Tutorials[0].SetActive(false);



    }


    






    //FUNCTION TO OPEN PAUSE MENU//

    public void OpenPauseMenu()
    {
        
        //ACTIVATE PAUSEMENU//
        SetUIState(UIMenuStates.Pause);



    }


    //BACK BUTTON FOR SETTINGS//


    public void ExitSettingMenu()
    {
        

       

        //ACTIVATE PAUSEMENU//
        PauseMenu.SetActive(true);


        //HIDE SETTINGSMENU//

         SettingsMenu.SetActive(false);




    }



    //RESUME GAME//
    public void ResumeGame()
    {
        
        //CALL THE SET UI STATE FUNCTION//

        SetUIState(UIMenuStates.Resume);

    
    }



    //FUNCTION TO GO BACK TO MAIN MENU//

    public void GoBackToTitleScreen()
    {
        
        SceneManager.LoadScene("Title");





    }



    //FUNCTION TO GO BACK TO MAIN MENU//

    public void GoBackToMenuOptions()
    {
        
        ControlsMenu.SetActive(false);





    }


    
}
