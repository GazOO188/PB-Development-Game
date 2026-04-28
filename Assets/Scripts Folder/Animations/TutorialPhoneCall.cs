using UnityEngine;
using System.Collections;

public class TutorialPhoneCall : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator PhoneAnim;

    [Header("State Control")]
    [SerializeField] public bool canStartPulsing = false, denied = false, accepted = false;

    
    [Header("GameObject")]
    [SerializeField] private GameObject Phonecall;


        
    [Header("Scripts")]
    [SerializeField] private InputHandler IH;

    

    public enum PhoneStates
    {
        Accept,
        Deny,
        Pulsing,
        CallBack,
    }

    public PhoneStates PS;

   void Awake()
   {
    Debug.Log("PHONE AWAKE: " + GameManager.Instance.FinalTaskCompleted);

    if (GameManager.Instance != null && GameManager.Instance.FinalTaskCompleted)
    {
        Debug.Log("DISABLING PHONE OBJECT");
        Phonecall.SetActive(false);
    }
   }   


    void Start()
    {
  
        StartCoroutine(Pulse());
     
        
    
    }
    
    void Update()
    {
        // You can keep this for future logic if needed
    }

    public void DetermineState(PhoneStates ps)
    {
        switch (ps)
        {
            case PhoneStates.Accept:

           if (GameManager.Instance.CanDisplayPhoneCallAgain)
           {
                 PhoneAnim.SetTrigger("Accept");
                 GameManager.Instance.CanDisplayPhoneCallAgain = false;
           }
            break;  
           
           
            case PhoneStates.Pulsing:
                PhoneAnim.SetBool("CanPulse", true);
                break;

            case PhoneStates.Deny:
            
                PhoneAnim.SetTrigger("Deny");
                break;

            case PhoneStates.CallBack:

               PhoneAnim.SetTrigger("CanCallAgain");
               PhoneAnim.SetBool("CanPulse", true);
               break;

            
                
            
        }
    }

    public void AcceptCall()
    {
        DetermineState(PhoneStates.Accept);
        PhoneAnim.SetBool("CanPulse", false);
        accepted = true;


        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        
    }

    public void DenyCall()
    {
    Debug.Log("Deny pressed");
    PhoneAnim.SetBool("CanPulse", false); 
    PhoneAnim.SetTrigger("Deny");
    StartCoroutine(ReturnCall());

    }
    
    public IEnumerator Pulse()
    {
        yield return new WaitForSeconds(1.1f);
        DetermineState(PhoneStates.Pulsing);
    }

    
    public IEnumerator ReturnCall()
    {
   
     yield return new WaitForSeconds(2f); // delay before calling again

      DetermineState(PhoneStates.CallBack);
   
    }
        
    }
