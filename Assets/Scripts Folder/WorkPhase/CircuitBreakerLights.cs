using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircuitBreakerLights : MonoBehaviour
{
    public Dictionary<string, List<GameObject>> allLights = new Dictionary<string, List<GameObject>>();
    [SerializeField] List<GameObject> kitchen = new List<GameObject>();
    [SerializeField] List<GameObject> bedroom = new List<GameObject>();
    void Start()
    {
        allLights.Add("Kitchen", kitchen);
        allLights.Add("Bedroom", bedroom);
    }

    public void LightSwitch(string roomName, bool flip)
    {
        if (allLights.ContainsKey(roomName))
            foreach (GameObject light in allLights[roomName])
            {
                light.SetActive(flip);
            }

    }
}
