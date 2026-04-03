using UnityEngine;
using System.Collections.Generic;


public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    public enum AllTools
    {
        None,
        //Envelope//
        WeatherStrip, CaulkGun, FoamSprayGun,
        //Electrical//
        CircuitBreaker, Outlets, OutletTester,
        //Mechanical
        Screwdriver, Wrench, AllenKeys
    }
    [SerializeField] List<GameObject> tools = new List<GameObject>();
    //[SerializeField] string[] itemNames;
    Dictionary<string, GameObject> toolDict = new Dictionary<string, GameObject>();
    //public List<string> inventory = new List<string>();
    //public List<string> currentItems = new List<string>();

    public Item currentItem = null;
    public AllTools currentTool = AllTools.None;
    public int panelIndex = 0;
    //[SerializeField] GameObject breaker, outlet, outletTester;
    //public bool itemIsMoving;

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
        //itemDict.Add("Weather Strip", itemNames[0]);
        //itemDict.Add("Caulk Gun", itemNames[1]);
        //itemDict.Add("Foam Gun", itemNames[2]);
        toolDict.Add("Outlet", tools[0]);
        toolDict.Add("Outlet Tester", tools[1]);
        toolDict.Add("Circuit Breaker", tools[2]);

        toolDict.Add("WeatherStrip", tools[3]);
        toolDict.Add("CaulkGun", tools[4]);
        //toolDict.Add("SprayFoam", tools[5]);
        //itemDict.Add("Wrench", itemNames[6]);
        //itemDict.Add("Screw Driver", itemNames[7]);
        //itemDict.Add("Allen Keys", itemNames[8]);

        for (int i = 0; i < tools.Count; i++)
            tools[i].SetActive(false);
    }

    void Update()
    {
        UpdateItem();
    }

    // public void UpdateInventory()
    // {
    //     foreach (string key in itemDict.Keys)
    //     {
    //         for (int i = 0; i < inventory.Count; i++)
    //         {
    //             if (key == inventory[i])
    //                 currentItems.Add(inventory[i]);
    //         }
    //     }
    // }

    public void UpdateItem()
    {
        ClearItems();

        if (currentItem == null)
        {
            if (currentTool is not AllTools.None)
            {
                currentTool = AllTools.None;
            }
            return;
        }

        if (currentItem.itemName == "Circuit Breaker" && currentTool is not AllTools.CircuitBreaker)
        {
            currentTool = AllTools.CircuitBreaker;

            if (panelIndex != 2) toolDict["Circuit Breaker"].SetActive(true);
        }

        if (currentItem.itemName == "Outlet" && currentTool is not AllTools.Outlets)
        {
            currentTool = AllTools.Outlets;

            toolDict["Outlet"].SetActive(true);
        }

        if (currentItem.itemName == "Outlet Tester" && currentTool is not AllTools.OutletTester)
        {
            currentTool = AllTools.OutletTester;

            toolDict["Outlet Tester"].SetActive(true);
        }


        //FOR WEATHERSTRIP//

        if (currentItem.itemName == "WeatherStrip" && currentTool is not AllTools.WeatherStrip)
        {

            currentTool = AllTools.WeatherStrip;

            toolDict["WeatherStrip"].SetActive(true);




        }


        //FOR CAULKGUN//

        if (currentItem.itemName == "CaulkGun" && currentTool is not AllTools.CaulkGun)
        {

            currentTool = AllTools.CaulkGun;

            toolDict["CaulkGun"].SetActive(true);



        }




    }

    void ClearItems()
    {
        foreach (GameObject tool in tools)
        {
            if (currentItem == null || (tool.activeInHierarchy && !tool.CompareTag(currentItem.itemName)))
            {
                tool.SetActive(false);
            }
        }
    }

}
