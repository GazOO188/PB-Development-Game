using UnityEngine;


public class CaulkGunDetection : MonoBehaviour
{
    //THIS SCRIPT IS FOR THE DETECTION OF THE CRACK ON THE WALL//


    //COMPLETE FINAL TASK//

    public bool isTouched = false;



    public CrackEdges parentCheck;


    public EnvelopePhase EP;
    



  
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    //FUNCTION TO DETECT COLLIDERS//

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Caulk") && EP.EnvelopeTask2Completed && EP.EnvelopeTask1Completed)
        {
            isTouched = true;
            parentCheck.CheckForCompletion();
        }
    }



}
