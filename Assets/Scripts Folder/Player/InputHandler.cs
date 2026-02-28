using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class InputHandler : MonoBehaviour
{
    // Source: https://www.youtube.com/watch?v=T2T82MWbbew
    public PlayerController player;

    public CollisionInteractions CI;
    [SerializeField] PlayerCamera playerCamera;

    public DialogueData Dialogue;

    
    public InputAction _move, _look, _crouch, _interact;

    void Awake()
    {
        _move = InputSystem.actions.FindAction("Move");
        _look = InputSystem.actions.FindAction("Look");
        _crouch = InputSystem.actions.FindAction("Crouch");
        _interact = InputSystem.actions.FindAction("Interact");

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




        //THIS IS FOR PRESSING E TO TALK//
        if (_interact.WasPressedThisFrame() && player.CanSeeBoss)
        {

           

            CI.DialogueText.enabled = true;

            CI.DialgouePanel.enabled = true;

            CI.WhoIsSpeakingTab.SetActive(true);

          
            StartCoroutine(CI.ShowDialgoueText(LanguageConversion.Instance.WordConverter(Dialogue.lines[0])));

            StartCoroutine(TemporarilyDisableRaycast());
            
        
        }
    }

    private IEnumerator TemporarilyDisableRaycast()
    {

        yield return new WaitForSeconds(0f);

        player.CanCast = false;
          
        CI.InteractText.enabled = false;

        yield return new WaitForSeconds(6.7f);


        player.CanCast = true;
          
        CI.InteractText.enabled = true;





    }


}
