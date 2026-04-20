using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UITrigger : MonoBehaviour
{
    //THIS SCRIPT IS TO MANAGE UI//

      

    [Header("UI Panel")]

    [SerializeField] public GameObject SettingsMenu;


    [Header("Pause Menu")]

    [SerializeField] public GameObject PauseMenu;



    [Header("Script References")]

    [SerializeField] public InputHandler IH;
    
    [SerializeField] public PlayerController PC;


    //ENUM TO SWITCH BETWEEN STATES


    public enum UIMenuStates
    {
        
        Resume,

        Pause,

        Settings,



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



        }





    }


    //FUNCTION FOR SETTINGS//
    public void OpenSettings()
    {
        

     SetUIState(UIMenuStates.Settings);


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


    
}
