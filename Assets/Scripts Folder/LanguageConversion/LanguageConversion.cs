using UnityEngine;
using System.Collections.Generic;

public class LanguageConversion : MonoBehaviour
{
    
    //THIS SCRIPT IS FOR CONVERTING BETWEEN ENGLISH AND SPANISH//

    public Dictionary<string, string> English = new Dictionary<string, string>();


    public Dictionary<string, string> Spanish = new Dictionary<string, string>();



    public static LanguageConversion Instance;

    
    void Awake()
    {
        //THIS IS FOR THE SETTINGS BUTTON//
        English.Add("Settings", "Settings");

        Spanish.Add("Settings", "Configuración");


        //THIS IS FOR THE ENGLISH TOGGLE OPTION//

        English.Add("English", "English");

        Spanish.Add("English", "Ingles");


        //THIS IS FOR THE HOW TO PLAY BUTTON//

        English.Add("How to Play", "How to Play");

        Spanish.Add("How to Play", "Cómo jugar");


        //THIS IS FOR THE TITLE OF THE GAME//

        English.Add("Building Energy Performance: The Game", "Building Energy Performance: The Game");

        Spanish.Add("Building Energy Performance: The Game", "Rendimiento Energético del Edificio: El Juego");


        //THIS IS FOR THE START && QUIT BUTTON//


        English.Add("Start", "Start");

        Spanish.Add("Start", "Iniciar");


        English.Add("Quit", "Quit");

        Spanish.Add("Quit", "Salir");


        //THIS IS FOR THE INSTRUCTIONS//

        English.Add("Instructions go here :)", "Instructions go here :)");

        Spanish.Add("Instructions go here :)", "Las instrucciones van aquí");



        //THIS IS FOR THE BOSS DIALOGUE//

        English.Add("Hmm... there could be a electrical problem wrong here... ", "Hmm... there could be a electrical problem wrong here... ");


        Spanish.Add("Hmm... there could be a electrical problem wrong here... ", "Hmm... podría haber algún problema eléctrico aquí.");


        //THIS IS FOR THE SPEAKER TAB//

        English.Add("Boss", "Boss");

        Spanish.Add("Boss", "Jefe");



        //THIS IS FOR INTERACTION BUTTONS//

        English.Add("Press E to Talk", "Press E to Talk");

        Spanish.Add("Press E to Talk", "Presiona E para hablar");
        

        
        
        Instance = this;
       

        


    
    
    
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //FUNCTION THAT CONVERTS WORDS FROM ENGLISH/SPANISH


    public string WordConverter(string Key)
    {

        //IF THE BOOL IS ENGLISHLANGUAGE TRUE, GIVE BACK THE ENGLISH VALUE, ELSE GIVE THE SPANISH VALUE//
        if (GameManager.Instance.englishLanguage)
        {
            
            

            return English[Key];

            
            
        }

        else
        {
            
            return Spanish[Key];


        }
        
    
    
    }



}
