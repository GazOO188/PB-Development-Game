using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    [SerializeField] private SoundLibrary sounds;
    [SerializeField] private float walkInterval = 0.5f;
    [SerializeField] private float crouchInterval = 0.8f; // slower when crouching
    [SerializeField] private LayerMask groundLayer;

    private float stepTimer = 0f;
    private CharacterController controller;
    private PlayerController playerController;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        // Don't play footsteps if player has no control
        if (!playerController.playerControl) return;

        if (IsMoving() && controller.isGrounded)
        {
            float interval = playerController._state == PlayerController.Stance.Crouch
                ? crouchInterval
                : walkInterval;

            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = interval;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    private void PlayFootstep()
    {
        string surface = GetSurface();
        var entry = sounds.Get(surface);
        if (entry == null) return;

        AudioManager.Instance.PlaySFXAt(
            entry.variants[Random.Range(0, entry.variants.Length)],
            transform.position,
            entry.volume
        );
    }

    private string GetSurface()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2f, groundLayer))
        {
            return hit.collider.tag switch
            {
                "Wood" => "footstep_wood",
                "Tile" => "footstep_tile",
                "Carpet" => "footstep_carpet",
                "Concrete" => "footstep_concrete",
                _ => "footstep_default"
            };
        }
        return "footstep_default";
    }

    private bool IsMoving()
    {
        // Only check horizontal movement, ignore vertical (gravity)
        Vector3 horizontalVelocity = new Vector3(
            controller.velocity.x,
            0f,
            controller.velocity.z
        );
        return horizontalVelocity.magnitude > 0.1f;
    }
}