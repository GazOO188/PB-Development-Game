using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public CharacterController characterController;
    public enum Stance
    {
        Stand,
        Crouch,
    }
    [SerializeField] PlayerCamera playerCamera;
    [SerializeField] CollisionInteractions CI;
    
    [Space]
    [Header("Movement")]
    public Stance _state;
    [SerializeField] float moveSpeed;
    [SerializeField] float gravity = -90f;
    [SerializeField] float airMultiplier;
    float standHeight = 2.0f;
    float crouchHeight = 1.0f;
    [Space]
    [Header("Ground Check")]
    [SerializeField] LayerMask isGround;
    [SerializeField] bool grounded = true;
    RaycastHit floorHit;

    [Space]
    [Header("Reticle")]
    [SerializeField] Image ret;
    Vector3 centerScreen = new Vector3(0.5f, 0.5f, 0f);

    [Header("Bool")]

    [SerializeField] public bool CanSeeBoss = false;
        
    


    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        _state = Stance.Stand;
    }

    void Update()
    {
        // Rotate player with camera
        transform.localEulerAngles = new Vector3(0f, playerCamera.transform.localEulerAngles.y, 0f);

        //cast to the center of the screen
        Ray ray = Camera.main.ViewportPointToRay(centerScreen);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            // Code here for interacting with object that player is looking at

            if (hit.collider.CompareTag("Boss"))
            {

                Debug.Log("Found Boss");

                CanSeeBoss = true;

                CI.InteractText.enabled = true;



            }



        }

        
         else
        {

                
                
                CI.InteractText.enabled = false;

                CanSeeBoss = false;

        }

        // Change height when transitioning from crouch to stand and vise versa 
        // Source: https://www.youtube.com/watch?v=NsSk58un8E0
        var currentHeight = transform.localScale.y;
        var normalizeHeight = currentHeight / standHeight;

        var rootTargetScale = new Vector3(1.0f, normalizeHeight, 1.0f);

        if (_state is Stance.Crouch)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, rootTargetScale, 1f - Mathf.Exp(-15f * Time.deltaTime));
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, 1f, 1f), 1f - Mathf.Exp(-15f * Time.deltaTime));
        }
    }

    public void Move(Vector2 movement)
    {
        Vector3 move = transform.forward * movement.y + transform.right * movement.x;
        move *= moveSpeed;

        // Limit movement in air
        if (!characterController.isGrounded)
        {
            move.x *= airMultiplier;
            move.z *= airMultiplier;
        }

        move.y += gravity;
        characterController.Move(move * Time.deltaTime);
    }

    public void Crouch()
    {
        if (_state is Stance.Stand) _state = Stance.Crouch;
        else _state = Stance.Stand;
    }

    void GroundCheck()
    {
        // Ground check -- Not using this but leaving it here for now
        grounded = Physics.BoxCast(transform.position, transform.localScale * 0.25f, Vector3.down, out floorHit, transform.rotation, 1.2f, isGround);
        //Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Color.magenta);
    }
}
