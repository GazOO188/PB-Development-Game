using UnityEngine;
using System.Collections;

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
        Sitting, 

        Standing, 

        Waving,

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
        case PossibleStates.Standing:
        {
            BossAnim.SetBool("CanStand", true);
            //BossAnim.SetBool("CanWave", false);
            break;
        }

        case PossibleStates.Waving:
        {
           // BossAnim.SetBool("Walk", false);
            BossAnim.SetBool("CanWave", true);

            BossTransform.LookAt(PlayerTransform.position);
            break;
        }

        case PossibleStates.Sitting:
        {
            BossAnim.SetBool("CanSit", true);
            BossAnim.SetBool("CanWave", false);
            BossAnim.SetBool("CanStand", false);
            break;
        }
    }
}


    public void UpdateStates()
    {
        
        //GET DISTANCE TO PLAYER
        float sqrDist = (PlayerTransform.position - BossTransform.position).sqrMagnitude;



        if(sqrDist < Range * Range)
        {
            
           StartCoroutine(ChangeToWave());

        
        }


        else
        {
            
            PS = PossibleStates.Sitting;

        }




    }


    private IEnumerator ChangeToWave()
    {
        
        yield return new WaitForSeconds(0f);


         PS = PossibleStates.Standing;
         
        
         yield return new WaitForSeconds(2f);
         PS = PossibleStates.Waving;




    }



}