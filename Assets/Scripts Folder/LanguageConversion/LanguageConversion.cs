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

        English.Add("Press WASD to Move", "Press WASD to Move");

        Spanish.Add("Press WASD to Move", "Presiona WASD para moverte.");


        English.Add("Use the Mouse to look around", "Use the Mouse to look around");

        Spanish.Add("Use the Mouse to look around", "Usa el ratón para mirar alrededor");



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

        Spanish.Add("Resident", "Residente");



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

        Spanish.Add("Salutations my good sir. I'm in need of assistance with an outlet in my bathroom.", "Saludos, mi buen señor. Necesito ayuda con un tomacorriente en mi baño.");



        English.Add("It does not work and I like to charge my devices when I am taking a shower.", "It does not work and I like to charge my devices when I am taking a shower.");

        Spanish.Add("It does not work and I like to charge my devices when I am taking a shower.", "No funciona y me gusta cargar mis dispositivos mientras me ducho.");


      
        English.Add("The outlet is right next to my sink it’s really easy to find. I would appreciate your assistance!", "The outlet is right next to my sink it’s really easy to find. I would appreciate your assistance!");

        Spanish.Add("The outlet is right next to my sink it’s really easy to find. I would appreciate your assistance!", "La toma de corriente está justo al lado del fregadero, es muy fácil de encontrar. ¡Te agradecería mucho tu ayuda!");
       
        //TASK 2//

        English.Add("Hello there sir, I’ve been having issues with an outlet in my kitchen by my microwave.", "Hello there sir, I’ve been having issues with an outlet in my kitchen by my microwave.");

        Spanish.Add("Hello there sir, I’ve been having issues with an outlet in my kitchen by my microwave.", "Buenos días, señor. Tengo un problema con un enchufe de la cocina, junto al microondas.");
        
        
       
        English.Add("I only use my microwave on that outlet. It was fine a couple days ago but then it just stopped working when I plugged in a microwave in the same socket.", "I only use my microwave on that outlet. It was fine a couple days ago but then it just stopped working when I plugged in a microwave in the same socket.");
        
        Spanish.Add("I only use my microwave on that outlet. It was fine a couple days ago but then it just stopped working when I plugged in a microwave in the same socket.", "Solo enchufo el microondas en esa toma de corriente. Hace un par de días funcionaba bien, pero dejó de funcionar en cuanto enchufé el microondas en la misma toma.");

       
       
        English.Add("It seems like the outlet is short circuited and I have no Idea why. Can you check it out?", "It seems like the outlet is short circuited and I have no idea why. Can you check it out?");

        Spanish.Add("It seems like the outlet is short circuited and I have no Idea why. Can you check it out?", "Parece que la toma de corriente tiene un cortocircuito y no tengo ni idea de por qué. ¿Podrías echarle un vistazo?");

        
        //TASK 3//

        English.Add("Well it’s about time you came.", "Well it’s about time you came.");

        Spanish.Add("Well it’s about time you came.", "Ya era hora de que vinieras.");

       
        English.Add("I thought I had to end up doing everything myself. You mind actually doing your job and fixing the light situation in my room.", "I thought I had to end up doing everything myself. You mind actually doing your job and fixing the light situation in my room.");

        Spanish.Add("I thought I had to end up doing everything myself. You mind actually doing your job and fixing the light situation in my room.", "Pensaba que al final tendría que hacerlo todo yo sola. ¿Te importaría hacer tu trabajo y arreglar el problema de la luz de mi habitación?");

       
        English.Add("It's been a pain having to go to the living room for entertainment. Get it done and quick, I’m missing critical hours on marvel rivals.", "It's been a pain having to go to the living room for entertainment. Get it done and quick I’m missing critical hours on marvel rivals.");
        
        Spanish.Add("It's been a pain having to go to the living room for entertainment. Get it done and quick, I’m missing critical hours on marvel rivals.", "Es un rollo tener que ir al salón para entretenerme. Arregladlo ya, me estoy perdiendo momentos cruciales en Marvel Rivals.");
        



        //EXTRA WORDS IN THE ELECTRIC SCENE: OBJECTIVES/PROMPTS/TASKS/TOOLS//

        English.Add("Press i to open inventory", "Press i to open inventory");

        Spanish.Add("Press i to open inventory", "Presiona i para abrir el inventario.");


        English.Add("Press SPACE to cycle", "Press SPACE to cycle");

        Spanish.Add("Press SPACE to cycle", "Presiona ESPACIO para alternar");


        English.Add("Objectives", "Objectives");

        Spanish.Add("Objectives", "Objetivos");


        //TASKS//

        English.Add("Repair the Broken Outlet", "Repair the Broken Outlet");
        
        Spanish.Add("Repair the Broken Outlet", "Repara la toma de corriente estropeada");


        English.Add("Fix the faulty breaker", "Fix the faulty breaker");

        Spanish.Add("Fix the faulty breaker", "Repara el disyuntor defectuoso");


       
        English.Add("Restore Bedroom power", "Restore Bedroom power");

        Spanish.Add("Restore Bedroom power", "Restablecer la electricidad en el dormitorio");

        
        English.Add("Report to the Resident", "Report to the Resident");

        Spanish.Add("Report to the Resident", "Informe para el residente");


       
        English.Add("Thank you for playing!", "Thank you for playing!");

        Spanish.Add("Thank you for playing!", "¡Gracias por jugar!");

       
        English.Add("Time's up!", "Time's up!");

        Spanish.Add("Time's up!", "¡Se acabó el tiempo!");

       
        English.Add("Return to Menu", "Return to Menu");

        Spanish.Add("Return to Menu", "Volver al menú");

       
        English.Add("Restart Level", "Restart Level");

        Spanish.Add("Restart Level", "Nivel de reinicio");



        //TOOLS//

        //ENVELOPE TOOLS//

        English.Add("Weather Strip", "Weather Strip");

        Spanish.Add("Weather Strip", "Burlete");


        English.Add("Caulk Gun", "Caulk Gun");

        Spanish.Add("Caulk Gun", "Pistola de silicona");


        English.Add("Foam Gun", "Foam Gun");

        Spanish.Add("Foam Gun", "Pistola de espuma");


        //ELECTRICAL TOOLS//

        English.Add("Outlet", "Outlet");

        Spanish.Add("Outlet", "toma de corriente");


        English.Add("Outlet Tester", "Outlet Tester");

        Spanish.Add("Outlet Tester", "Probador de enchufes");


        English.Add("Circuit Breaker", "Circuit Breaker");

        Spanish.Add("Circuit Breaker", "Disyuntor");




        //MECHANICAL TOOLS//

        English.Add("Wrench", "Wrench");

        Spanish.Add("Wrench", "Llave inglesa");


        English.Add("Screwdriver", "Screwdriver");

        Spanish.Add("Screwdriver", "Destornillador");


        English.Add("Allen Keys", "Allen Keys");

        Spanish.Add("Allen Keys", "Llaves Allen");



        //CIRCUIT BREAKER TOOLTIP//

        English.Add("Single-Pole Breaker: Press 1", "Single-Pole Breaker: Press 1");

        Spanish.Add("Single-Pole Breaker: Press 1", "Disyuntor unipolar: Presiona 1");



        English.Add("Double-Pole Breaker: Press 2", "Double-Pole Breaker: Press 2");

        Spanish.Add("Double-Pole Breaker: Press 2", "Disyuntor bipolar: Presiona 2");



        English.Add("Select Breaker", "Select Breaker");

        Spanish.Add("Select Breaker", "Selecciona el disyuntor");


        English.Add("Left Click to turn on/off", "Left Click to turn on/off");

        Spanish.Add("Left Click to turn on/off", "Clic izquierdo para encender/apagar");


        English.Add("Right Click to replace", "Right Click to replace");

        Spanish.Add("Right Click to replace", "Clic derecho para reemplazar");


        English.Add("Bedroom", "Bedroom");

        Spanish.Add("Bedroom", "Dormitorio");


        English.Add("Kitchen", "Kitchen");

        Spanish.Add("Kitchen", "Cocina");
        


       
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
