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


        
        
        English.Add("Welcome! It looks like we'll be dealing with a envelope issue today...", "Welcome! It looks like we'll be dealing with a envelope issue today...");

        Spanish.Add("Welcome! It looks like we'll be dealing with a envelope issue today...", "¡Bienvenido! Parece que hoy tendremos un problema con un sobre…");



        //THIS IS FOR THE SPEAKER TAB//

        English.Add("Boss", "Boss");

        Spanish.Add("Boss", "Jefe");



        //THIS IS FOR RESIDENT SPEAKER TAB//

        English.Add("Resident", "Resident");



        //THIS IS FOR INTERACTION BUTTONS//

        English.Add("Press E to Talk", "Press E to Talk");

        Spanish.Add("Press E to Talk", "Presiona E para hablar");


        //THIS IS FOR THE FIRST RESIDENT//

        English.Add("Hi! I have a heating problem in my room.","Hi! I have a heating problem in my room.");

        English.Add("The heat comes and go. It's hard to get a good night rest.", "The heat comes and go. It's hard to get a good night rest.");

        English.Add("The furnace is downstairs.", "The furnace is downstairs.");


        //THIS IS FOR THE SECOND RESIDENT//

        English.Add("Hi! Lately the Kitchen has been really cold.", "Hi! Lately the Kitchen has been really cold.");

        English.Add("I constantly feel a draft coming the window.", "I constantly feel a draft coming the window.");

        English.Add("I wonder what it could be...", "I wonder what it could be...");



        //THIS IS FOR THE THIRD RESIDENT//

        English.Add("Salutations my good sir. I'm in need of assistance with an outlet in my bathroom.","Salutations my good sir. I'm in need of assistance with an outlet in my bathroom.");

        English.Add("It does not work and I like to charge my devices when I am taking a shower.", "It does not work and I like to charge my devices when I am taking a shower.");

        English.Add("The outlet is right next to my sink it’s really easy to find. I would appreciate your assistance!", "The outlet is right next to my sink it’s really easy to find. I would appreciate your assistance!");
        //TASK 2//

        English.Add("Hello there sir, I’ve been having issues with an outlet in my kitchen by my microwave.", "Hello there sir, I’ve been having issues with an outlet in my kitchen by my microwave.");

        English.Add("I only use my microwave on that outlet. It was fine a couple days ago but then it just stopped working when I plugged in a microwave in the same socket.", "I only use my microwave on that outlet. It was fine a couple days ago but then it just stopped working when I plugged in a microwave in the same socket.");

        English.Add("It seems like the outlet is short circuited and I have no Idea why. Can you check it out?", "It seems like the outlet is short circuited and I have no idea why. Can you check it out?");


        //TASK 3//

        English.Add("Well it’s about time you came.", "Well it’s about time you came.");

        English.Add("I thought I had to end up doing everything myself. You mind actually doing your job and fixing the light situation in my room.", "I thought I had to end up doing everything myself. You mind actually doing your job and fixing the light situation in my room.");

        English.Add("It's been a pain having to go to the living room for entertainment. Get it done and quick, I’m missing critical hours on marvel rivals.", "It's been a pain having to go to the living room for entertainment. Get it done and quick I’m missing critical hours on marvel rivals.");

        

        
        
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

        Key = Key.Trim();
        
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
