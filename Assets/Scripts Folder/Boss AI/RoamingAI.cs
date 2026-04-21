using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class RoamingAI : MonoBehaviour
{
    // REFERENCE TO THE PLAYER GAMEOBJECT
    [SerializeField] public GameObject Player;

    // REFERENCE TO THE NAVMESHAGENT
    public NavMeshAgent navMesh;

    // LIST OF WAYPOINTS FOR THE BOSS TO WALK TO
    [SerializeField] public List<Transform> PathPoints = new List<Transform>();

    // SPEED AT WHICH THE BOSS ROTATES
    [SerializeField] public float rotationSpeed;

    // REFERENCE TO THE ANIMATOR COMPONENT
    [SerializeField] public Animator anim;

    // THE AMOUNT OF TIME THE BOSS WILL IDLE AT EACH WAYPOINT
    [SerializeField] public float waitTime = 2f;

    // THE INDEX OF THE CURRENT WAYPOINT
    private int CurrentPoint;

    // BOOLEAN TO CHECK IF THE NPC IS CURRENTLY WAITING
    public bool isWaiting = false;

    public AnimationManager AM;

    void Awake()
    {
        // GETS THE REFERENCE TO THE NAVMESHAGENT COMPONENT
        navMesh = GetComponent<NavMeshAgent>();

        // FINDS THE PLAYER GAMEOBJECT IN THE SCENE
        Player = GameObject.Find("Player");
    }

    void Start()
    {
        // CHECK IF THERE ARE WAYPOINTS IN THE LIST
        if (PathPoints.Count > 0)
        {
            // SET THE INITIAL DESTINATION TO THE FIRST WAYPOINT
            navMesh.SetDestination(PathPoints[CurrentPoint].position);

            // ALLOW THE NAVMESHAGENT TO MOVE
            navMesh.isStopped = false;

            // PLAY WALKING ANIMATION
            anim.SetBool("Walk", true);
        }
    }

    void Update()
    {
        // SMOOTHLY ROTATE THE BOSS TOWARDS MOVEMENT DIRECTION
        RotateTowardsWaypoint();

        // CHECK IF THE BOSS SHOULD FOLLOW THE NEXT WAYPOINT
        FollowPath();


        //IF WAVING STOP MOVING//
       // if (AM.PS == AnimationManager.PossibleStates.waving)
       // {
            
            //navMesh.isStopped = true;

       // }

        //else
       // {
            
           // navMesh.isStopped = false;


      //  }
    }

    void FollowPath()
    {
        // ONLY CHECK FOR NEXT WAYPOINT IF NOT CURRENTLY WAITING
        if (!isWaiting && navMesh.remainingDistance < 0.5f && !navMesh.pathPending)
        {
            // STOP THE NAVMESHAGENT TO IDLE
            navMesh.isStopped = true;

            // PLAY IDLE ANIMATION
           // anim.Play("Idle", 0, 0f);

            // START THE IDLE COROUTINE BEFORE MOVING TO NEXT WAYPOINT
            StartCoroutine(IdleThenMoveToNextWaypoint());
        }
    }

    // COROUTINE TO MAKE THE BOSS IDLE BEFORE MOVING TO THE NEXT WAYPOINT
    public IEnumerator IdleThenMoveToNextWaypoint()
    {
        // SET WAITING BOOL TO TRUE
        isWaiting = true;

        // WAIT AT THE CURRENT WAYPOINT
        yield return new WaitForSeconds(waitTime);

        // INCREMENT THE CURRENT WAYPOINT INDEX
        CurrentPoint++;

        // LOOP BACK TO THE FIRST WAYPOINT IF LAST WAS REACHED
        if (CurrentPoint >= PathPoints.Count)
        {
            CurrentPoint = 0;
        }

        // SET THE NEXT DESTINATION FOR NAVMESHAGENT
        navMesh.SetDestination(PathPoints[CurrentPoint].position);

        // ALLOW NAVMESHAGENT TO MOVE AGAIN
        navMesh.isStopped = false;

        // RESUME WALKING ANIMATION
        anim.SetBool("Walk", true);

        // CLEAR WAITING FLAG
        isWaiting = false;
    }

    // FUNCTION TO ROTATE THE BOSS TOWARDS MOVEMENT DIRECTION
    void RotateTowardsWaypoint()
    {
        // GET CURRENT VELOCITY VECTOR
        Vector3 facingDir = navMesh.velocity;

        // IF MOVING, ROTATE SMOOTHLY TOWARDS THE MOVEMENT DIRECTION
        if (facingDir.sqrMagnitude > 0.01f)
        {
            // CALCULATE TARGET ROTATION
            Quaternion targetRotation = Quaternion.LookRotation(facingDir);

            // APPLY SMOOTH ROTATION
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}