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



        
    [Header("Miscellaneous")]

    [SerializeField] public GameObject PhoneCasing;
 

    //ENUM TO SWITCH BETWEEN STATES


    public enum UIMenuStates
    {
        
        Resume,

        Pause,

        Settings,

        HowToPlay,



    }


    public UIMenuStates CurrentUIState;

    private bool volumeOpen = true;

    [SerializeField] private GameObject volumeIcon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       


        //SET UI PANELS TO BE HIDDEN UNTIL CALLED//
        SettingsMenu.SetActive(false);

        
        PauseMenu.SetActive(false);

        PhoneCasing.SetActive(false);




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
            PhoneCasing.SetActive(false);

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

            PhoneCasing.SetActive(true);

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
        
        //volumeOpen = !volumeOpen;
        //volumeIcon.SetActive(volumeOpen);
        SetUIState(UIMenuStates.Settings);


    }

    //FUNCTION TO OPEN CONTROLS MENU WITH THE CONTROLS AND HOW TO PLAY TAB//
    public void OpenControls()
    {
        
        //ACTIVATE HOW TO PLAY MENU//
        SetUIState(UIMenuStates.HowToPlay);



    }

    //FUNCTIION TO ACTIVATE AND DEACTIVATE ALL TUTORIALS//

    public void ShowTutorial(int index)
    {
    
    for (int i = 0; i < Tutorials.Count; i++)
    {
        Tutorials[i].SetActive(i == index);
    }
   
    }



    //FUNCTION TO OPEN HOW TO PLAY MENU//

    public void TutorialGuide()
    {
        
        //ACTIVATE HOW TO PLAY FROM TAB//

       Tutorial.SetActive(true);

       ShowTutorial(0);


    }


    //FUNCTION TO OPEN TUTORIAL 1 MENU//

    public void OpenTutorialOne()
    {
        
        //ACTIVATE HOW TO PLAY FROM TAB//

        ShowTutorial(1);



    }




    //FUNCTION TO GO BACK TO TUTORIAL 1 MENU//

    public void GoBacktoTutorialOne()
    {
        
      
        ShowTutorial(0);


    }




    //FUNCTION TO OPEN TUTORIAL 2 MENU//

    public void GoBacktoTutorialTwo()
    {
        
       
         ShowTutorial(1);



    }




    //FUNCTION TO OPEN TUTORIAL 1 MENU//

    public void OpenTutorialTwo()
    {
        
        //ACTIVATE HOW TO PLAY FROM TAB//
       
         ShowTutorial(2);



    }


    public void OpenTutorialFour()
    {
    

         ShowTutorial(3);


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

        PhoneCasing.SetActive(true);


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
