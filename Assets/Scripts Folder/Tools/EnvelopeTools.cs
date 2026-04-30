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

    bool isPressing = triggerValue > 0.1f; 

    // SPRAY THE CAULK WHILE PRESSING//
    if (isPressing)
    {
        if (Time.time - CG.lastUsedTime >= CG.sprayRate)
        {
            CG.ShootCaulk();
            CG.lastUsedTime = Time.time;
        }
    }
    else
    {
        //RESET WHEN NOT PRESSING//
        CG.hasLastPoint = false;
    }
}

}


    
  
    
    
