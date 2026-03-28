using UnityEngine;
using UnityEngine.InputSystem;

public class EnvelopeTools : MonoBehaviour
{
    //REFERENCE TO PLAYER INVENTORY//

   
    
    public WeatherStripTool WST;
    public InputHandler IH;
    
    

    void Update()
    {
        // Press T to activate WeatherStrip
        if (PlayerInventory.Instance.currentTool == PlayerInventory.AllTools.WeatherStrip)
        {
            HandleEachTool();
            Debug.Log("WeatherStrip tool activated");
        }

    }

    void HandleEachTool()
    {
        switch (PlayerInventory.Instance.currentTool)
        {
            case PlayerInventory.AllTools.WeatherStrip:
            {
            WST.HandleWeatherStrip();
            break;
            }
    }
}
}
    
  
    
    
