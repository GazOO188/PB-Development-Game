using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    // Source: https://www.youtube.com/watch?v=T2T82MWbbew
    public PlayerController player;
    [SerializeField] PlayerCamera playerCamera;
    InputAction _move, _look, _crouch;

    void Awake()
    {
        _move = InputSystem.actions.FindAction("Move");
        _look = InputSystem.actions.FindAction("Look");
        _crouch = InputSystem.actions.FindAction("Crouch");

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 movement = _move.ReadValue<Vector2>();
        player.Move(movement);

        Vector2 look = _look.ReadValue<Vector2>();
        playerCamera.Rotate(look);

        if (_crouch.WasPressedThisFrame() && player.characterController.isGrounded)
            player.Crouch();
    }
}
