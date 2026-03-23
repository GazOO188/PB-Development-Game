using UnityEngine;
using UnityEngine.InputSystem;

public class EnvelopeTools : MonoBehaviour
{
    public enum TypesofEnvelopeTools { None, WeatherStrip, CaulkGun, FoamSprayGun }

    public TypesofEnvelopeTools CurrentTool = TypesofEnvelopeTools.None;

    public WeatherStripTool WST;
    public InputHandler IH;

    void Update()
    {
        // Press T to activate WeatherStrip
        if (IH._WeatherStrip.WasPressedThisFrame())
        {
            CurrentTool = TypesofEnvelopeTools.WeatherStrip;
            Debug.Log("WeatherStrip tool activated");
        }

        HandleEachTool();
    }

    void HandleEachTool()
    {
        switch (CurrentTool)
        {
            case TypesofEnvelopeTools.WeatherStrip:
                WST.HandleWeatherStrip(); // handles mouse input inside WeatherStripTool
                break;
        }
    }
}
    
  
    
    
