using UnityEngine;
using UnityEngine.InputSystem;

public class EnvelopeTools : MonoBehaviour
{
    //REFERENCE TO PLAYER INVENTORY//

   
    
    public WeatherStripTool WST;
    
    public CaulkGun CG;
    
    public InputHandler IH;

    public SprayFoam SF;



    void Update()
    {

        switch (PlayerInventory.Instance.currentTool)
        {
            
           case PlayerInventory.AllTools.WeatherStrip:
           {
                    
            
                // Press T to activate WeatherStrip
                if (PlayerInventory.Instance.currentTool == PlayerInventory.AllTools.WeatherStrip)
                 {
                     HandleWeatherStripTool();
                     Debug.Log("WeatherStrip tool activated");
                   
                 }

                 break;
                
           }

            case PlayerInventory.AllTools.CaulkGun:
            {

                if(PlayerInventory.Instance.currentTool == PlayerInventory.AllTools.CaulkGun)
                {
                        

                    HandleCaulkGunTool();
                    Debug.Log("CaulkGun Activated");


                }   

                break; 


            }

            case PlayerInventory.AllTools.FoamSprayGun:
            {
                    
                if(PlayerInventory.Instance.currentTool == PlayerInventory.AllTools.FoamSprayGun)
                {
                        
                    SF.HandleFoamSpray();
                    SF.UpdateFoamGrowth();
                   

                }
               
                break;

            }
         

        }

       
    }

    void HandleWeatherStripTool()
    {
       
      WST.HandleWeatherStrip();
          
    }
    
    

    void HandleCaulkGunTool()
    {
    if (IH._CaulkGun == null) return;

    // Read current trigger value
    float triggerValue = IH._CaulkGun.ReadValue<float>();

    // Reset hasLastPoint on first frame pressed
    if (IH._CaulkGun.triggered)
    {
        CG.hasLastPoint = false; // start a new line
    }

    // While holding the trigger, spray caulk
    if (triggerValue > 0f)
    {
        if (Time.time - CG.lastUsedTime >= CG.sprayRate)
        {
            CG.ShootCaulk();
            CG.lastUsedTime = Time.time;
        }
    }

    // Reset hasLastPoint on release
    if (IH._CaulkGun.phase == InputActionPhase.Canceled)
    {
        CG.hasLastPoint = false;
    }
    }

}


    
  
    
    
