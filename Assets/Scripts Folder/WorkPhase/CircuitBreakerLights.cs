using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircuitBreakerLights : MonoBehaviour
{
    public Dictionary<string, List<Light>> allLights = new Dictionary<string, List<Light>>();
    [SerializeField] List<Light> kitchen = new List<Light>();
    [SerializeField] List<Light> bedroom = new List<Light>();
    float startIntensity = 13.25f;
    void Start()
    {
        allLights.Add("Kitchen", kitchen);
        allLights.Add("Bedroom", bedroom);
    }

    public void LightSwitch(string roomName, bool flip)
    {
        if (allLights.ContainsKey(roomName))
            foreach (Light light in allLights[roomName])
            {
                light.intensity = flip ? startIntensity : 0f;
            }
    }
}
