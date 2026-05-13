using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public CharacterController characterController;
    Outline outline;
    public enum Stance
    {
        Stand,
        Crouch,
    }
    [SerializeField] PlayerCamera playerCamera;
    [SerializeField] CollisionInteractions CI;
    [SerializeField] InputHandler IH;


    [Space]
    [Header("Movement")]
    public bool playerControl = true;
    public Stance _state;
    [SerializeField] float moveSpeed;
    float walkSpeed, crouchSpeed;
    [SerializeField] float gravity = -90f;
    [SerializeField] float airMultiplier;
    float standHeight = 1.0f;
    float crouchHeight = 0.62f;
    [Space]
    [Header("Ground Check")]
    [SerializeField] LayerMask isGround;
    [SerializeField] bool grounded = true;
    RaycastHit floorHit;

    [Space]
    [Header("Reticle")]
    [SerializeField] Image ret;
    Vector3 centerScreen = new Vector3(0.5f, 0.5f, 0f);
    float raycastDist = 3.0f;

    [Header("Bool")]

    [SerializeField] public bool CanSeeBoss = false, canSummonInventory = true;
    [Space]

    [Header("Inventory")]
    public GameObject inventory;
    [Space]
    public bool toolInUse;

    //FOR THE RESIDENTS//

    [SerializeField] public bool ResidentOneSeen = false, ResidentTwoSeen = false, ResidentThreeSeen = false;

    [SerializeField] public bool CanCast = true;
    //[SerializeField] LayerMask layerMask;


    [SerializeField] public GameObject CurrentlyHit;



    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        //PlayerInventory.Instance.UpdateInventory();
        characterController = GetComponent<CharacterController>();
        _state = Stance.Stand;

        walkSpeed = moveSpeed;
        crouchSpeed = moveSpeed * 0.45f;
    }

    void Update()
    {
        if (GameManager.Instance.GameOver) return;

        if (Input.GetKeyDown(KeyCode.I) && !toolInUse && canSummonInventory)
        {

            if (!playerControl)
            {
                GameObject[] tools = GameObject.FindGameObjectsWithTag("Tool UI");
                foreach (GameObject tool in tools)
                {
                    tool.GetComponent<Item>().CloseUI();
                }
            }


            InventoryScreen();
        }



        if (!playerControl) return;
        // Rotate player with camera
        transform.localEulerAngles = new Vector3(0f, playerCamera.transform.localEulerAngles.y, 0f);

        //cast to the center of the screen
        Ray ray = Camera.main.ViewportPointToRay(centerScreen);
        RaycastHit hit = new RaycastHit();

        if (CanCast)
        {
            if (Physics.Raycast(ray, out hit, raycastDist))
            {
                //WHAT THE PLAYER IS CURRENTLY LOOKING AT//
                CurrentlyHit = hit.collider.gameObject;

                // Circuit Breaker
                if (hit.collider.CompareTag("Circuit Breaker") && PlayerInventory.Instance.currentItem != null && PlayerInventory.Instance.currentItem.itemName == "Circuit Breaker" && hit.transform.gameObject.GetComponent<Renderer>().material.color != Color.gray)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        GameObject.Find("Circuit Manager").GetComponent<CircuitBreaker>().UpdateCircuit(hit.collider.gameObject);
                    }
                }

                // Outlet
                if (hit.collider.CompareTag("Outlet") && hit.transform.parent.GetComponent<Outlet>().canBeTested && PlayerInventory.Instance.currentItem != null)
                {
                    if (PlayerInventory.Instance.currentItem.itemName == "Outlet" && hit.transform.gameObject.GetComponent<Renderer>().material.color != Color.white)
                    {
                        //ShowOutline(hit);

                        if (Input.GetMouseButtonDown(0))
                        {
                            GameObject.Find("Outlet Manager").GetComponent<Outlet>().UpdateOutlet(hit.collider.gameObject);

                            //RemoveOutline();
                        }
                    }
                    else if (PlayerInventory.Instance.currentItem.itemName == "Outlet Tester" && !hit.transform.parent.GetComponent<Outlet>().outletTested)
                    {
                        //ShowOutline(hit);

                        if (Input.GetMouseButtonDown(0))
                        {
                            StartCoroutine(GameObject.Find("Outlet Manager").GetComponent<OutletTester>().TestOutlets(hit.transform.gameObject));

                            //RemoveOutline();
                        }
                    }
                }

                // MAKE THE DETECTION A BIT MORE NEATER//
                CanSeeBoss = hit.collider.CompareTag("Boss");
                ResidentOneSeen = hit.collider.CompareTag("Resident 1");

                //ENABLED INTERACT TEXT//
                if (CanSeeBoss || ResidentOneSeen)
                {
                    CI.InteractText.enabled = true;
                }
                else
                {
                    CI.InteractText.enabled = false;
                }
            }
            else
            {
                //NOT LOOKING AT ANYTHING CLEARING THE VARIABLE//
                CurrentlyHit = null;

                if (outline != null) RemoveOutline();

                CI.InteractText.enabled = false;
                ResidentOneSeen = false;
                ResidentTwoSeen = false;
                ResidentThreeSeen = false;
                CanSeeBoss = false;
            }
        }


        // Change height when transitioning from crouch to stand and vise versa 
        // Source: https://www.youtube.com/watch?v=NsSk58un8E0

        if (_state is Stance.Crouch)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, crouchHeight, 1f), 1f - Mathf.Exp(-15f * Time.deltaTime));

            if (moveSpeed != crouchSpeed) moveSpeed = crouchSpeed;
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, standHeight, 1f), 1f - Mathf.Exp(-15f * Time.deltaTime));
            if (moveSpeed != walkSpeed) moveSpeed = walkSpeed;
        }
    }


    void ShowOutline(RaycastHit hit)
    {
        if (outline == null)
        {
            outline = hit.collider.GetComponent<Outline>();
            if (!outline.enabled) outline.enabled = true;
            //outline.OutlineWidth = 10f;
        }
    }

    void RemoveOutline()
    {
        //outline.OutlineWidth = 0;
        outline.enabled = false;
        outline = null;
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

    public void InventoryScreen()
    {
        playerControl = !playerControl;

        inventory.SetActive(!playerControl);

        Cursor.visible = !playerControl;

        if (playerControl) Cursor.lockState = CursorLockMode.Locked;

        else Cursor.lockState = CursorLockMode.None;
    }







}
