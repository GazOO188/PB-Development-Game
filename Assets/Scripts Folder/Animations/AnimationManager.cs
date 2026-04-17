using UnityEngine;

public class AnimationManager : MonoBehaviour
{

    //REFERENCE TO ANIMATOR//

    [SerializeField] public Animator BossAnim;


    //REFERENCE TO TRANSFORMS OF PLAYER && BOSS NPC//

    [SerializeField] public Transform PlayerTransform, BossTransform;


    //DISTANCE CHECK//
    [SerializeField] private float Range = 4f;


    //REFERENCE TO NAVMESH SCRIPT//

    [SerializeField] public RoamingAI RAI;



    //TRACK STATES//

    public enum PossibleStates
    {
        walking, 

        waving, 

        stopping,

    }


    public PossibleStates PS;


    void Awake()
    {
        
        RAI = GetComponent<RoamingAI>();

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {

      UpdateStates();

      UpdateAnimations();

        //MAKE THE BOSS NPC FACE THE PLAYER//

       // BossTransform.LookAt(PlayerTransform.position);
        
    }


    //THIS LOCKS THE ROTATION OF THE BOSS NPC//
    void LateUpdate()
    {   
        Vector3 euler = BossTransform.eulerAngles;

        // PREVENTS THE BOSS FROM TILTING BACKWARD WHEN GETTING TOO CLOSE//
        euler.x = 0; 
        euler.z = 0;
   
        BossTransform.eulerAngles = euler;
    
    }


    public void UpdateAnimations()
    {
    
     switch (PS)
    {
        case PossibleStates.walking:
        {
            BossAnim.SetBool("Walk", true);
            BossAnim.SetBool("CanWave", false);
            break;
        }

        case PossibleStates.waving:
        {
            BossAnim.SetBool("Walk", false);
            BossAnim.SetBool("CanWave", true);

            BossTransform.LookAt(PlayerTransform.position);
            break;
        }

        case PossibleStates.stopping:
        {
            BossAnim.SetBool("Walk", false);
            BossAnim.SetBool("CanWave", false);
            break;
        }
    }
}


    public void UpdateStates()
    {
        
        float sqrDist = (PlayerTransform.position - BossTransform.position).sqrMagnitude;


        if (RAI.isWaiting)
        {
            
            PS = PossibleStates.stopping;
           
            return;

        }


        if(sqrDist < Range * Range)
        {
            
            PS = PossibleStates.waving;

        
        }


        else
        {
            
            PS = PossibleStates.walking;

        }




    }



}