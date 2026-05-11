using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TaskTracker : MonoBehaviour
{
    //THIS SCRIPT IS FOR TRACKING THE ELECTRICAL TASKS//

      [Header("Scripts References")]
      public Outlet outlet;

      [Header("GameObject")]
      public GameObject Bar;

      
      [Header("TextMeshPro")]
      public TextMeshProUGUI Completion;


      public GameObject[] ProgressBarParts;


      public bool CanTrack = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //IF IN THE ENVELOPE SCENE, SHUT OFF//
        if (GameManager.Instance.inEnvelopeScene)
        {
            
            Bar.SetActive(false);

        }
       
    }

    // Update is called once per frame
    void Update()
    {

        if (!CanTrack && !GameManager.Instance.FinalTaskCompleted && !GameManager.Instance.inEnvelopeScene)
        {
            
            StartCoroutine(TrackProgressForFirstTask(ProgressBarParts, Bar, Completion));

            CanTrack = true;



        }



    }



public IEnumerator TrackProgressForFirstTask(GameObject[] ProgressMeter, GameObject Bar, TextMeshProUGUI completion)
{
    yield return new WaitUntil(() => outlet.outletTested);
    //IF THE OUTLET IS TESTED//
    ProgressMeter[0].SetActive(true);
    completion.text = "1/2";

    //IF REPLACING THE OUTLET//
    yield return new WaitUntil(() => outlet.complete);
    
    ProgressMeter[0].SetActive(false);
        
    ProgressMeter[1].SetActive(true);

    completion.text = "2/2";

    yield return new WaitForSeconds(2.3f);

    Bar.SetActive(false);
}

}