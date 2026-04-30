using UnityEngine;
using System.Collections.Generic;

public class CrackEdges : MonoBehaviour
{   
    //STORES EDDGES//

    public List<CaulkGunDetection> edges = new List<CaulkGunDetection>();


    public bool Fullysealed = false;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void CheckForCompletion()
    {
        

        foreach(CaulkGunDetection edge in edges)
        {


            if (!edge.isTouched)
            {
                
                return;



            }



    

        }


       FullyCompleted();
    }



    public void FullyCompleted()
    {
        Debug.Log("Crack sealed!");

        Fullysealed = true;
        

    }
}
