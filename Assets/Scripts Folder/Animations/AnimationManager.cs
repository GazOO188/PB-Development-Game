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
        PlayWaveAnimation();


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



    public void PlayWaveAnimation()
    {
        

        //THESE TURN TRANSFORM INTO VECTOR3 TO USE FOR DIST CALCUATION//
        Vector3 PlayerPos = PlayerTransform.position;

        Vector3 BossPos = BossTransform.position;


        //GETS THE VECTOR FROM THE BOSS TO THE PLAYER//
        Vector3 DirToBoss = PlayerPos - BossPos;


    
        //COMPARE SQUARED DISTANCE FOR OPTIMIZATION//

        if(DirToBoss.sqrMagnitude < Range * Range)
        {
            
       
        BossAnim.SetBool("CanWave", true);

        BossAnim.SetBool("Walk", false);

        //TEMPORARILY STOP NAVMESH//
        RAI.navMesh.isStopped = true;


        BossTransform.LookAt(PlayerTransform.position);



        }


        else 
        {

        BossAnim.SetBool("CanWave", false);

       
        
        BossAnim.SetBool("Walk", true);

        //TEMPORARILY UNSTOP NAVMESH//
        RAI.navMesh.isStopped = false;



        }



        if (RAI.isWaiting)
        {

            BossAnim.SetBool("Walk", false);

                
        }





    }





}
