using UnityEngine;
using System.Collections.Generic;


public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    [SerializeField] GameObject[] itemPrefabs;
    [SerializeField] string[] itemNames;
    Dictionary<string, string> itemDict = new Dictionary<string, string>();
    public List<string> inventory = new List<string>();
    public List<string> currentItems = new List<string>();

    public ItemSelect currentItem = null;
    public bool itemIsMoving;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        itemDict.Add("Weather Strip", itemNames[0]);
        itemDict.Add("Caulk Gun", itemNames[1]);
        itemDict.Add("Foam Gun", itemNames[2]);
        itemDict.Add("Outlet", itemNames[3]);
        itemDict.Add("Outlet Tester", itemNames[4]);
        itemDict.Add("Circuit Breaker", itemNames[5]);
        itemDict.Add("Wrench", itemNames[6]);
        itemDict.Add("Screw Driver", itemNames[7]);
        itemDict.Add("Allen Keys", itemNames[8]);
    }

    public void UpdateInventory()
    {
        foreach (string key in itemDict.Keys)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (key == inventory[i])
                    currentItems.Add(inventory[i]);
            }
        }
    }

}
