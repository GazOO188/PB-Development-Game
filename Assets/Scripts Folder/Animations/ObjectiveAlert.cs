using UnityEngine;
using System.Collections;

public class ObjectiveAlert : MonoBehaviour
{
    //THIS SCRIPT IS THE NOTIFICATION ALERT SYMBOL AT THE BEGINNING OF THE WORKPHASE//

    public Animator Anim;
    
    public GameObject Objectives, Task1, Task2, Task3, EP;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Objectives.SetActive(false);

        Task1.SetActive(false);

        Task2.SetActive(false);

        Task3.SetActive(false);


        Anim = GameObject.Find("Exclamation Points").GetComponent<Animator>(); 


        EP.SetActive(false);

       
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }


    //FUNCTION TO TRIGGER THE OBJECTIVE ANMIATION//
    public IEnumerator TriggerAnimation()
    {
        

        EP.SetActive(true);
       
        yield return new WaitForSeconds(0f);

        
        Anim.Play("AlertNotification");



        yield return new WaitForSeconds(2f);


        Anim.Play("SlidetoLeft");



        yield return new WaitForSeconds(2f);

        Anim.enabled = false;
        
        Objectives.SetActive(true);

        EP.SetActive(false);

        

        
        yield return new WaitForSeconds(1f);

        Task1.SetActive(true);


        yield return new WaitForSeconds(1f);

        Task2.SetActive(true);


        yield return new WaitForSeconds(1f);

        Task3.SetActive(true);

        









    }




}
