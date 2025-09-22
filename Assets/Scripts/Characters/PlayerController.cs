using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    // References
    private PlayerInput _playerInput;
    private Movement _playerMovement;
    private SpriteAnimator _playerAnimator;
    private GrabAbility _pickupAbility;

    // GrabDrop Variables
    private bool _isUsingClaw = false;

    private void Awake() {
        _playerInput = new PlayerInput(); 
        _playerMovement = GetComponent<Movement>();
        _playerAnimator = GetComponent<SpriteAnimator>();
        _pickupAbility = GetComponentInChildren<GrabAbility>();
    }

    private void OnEnable() {
        _playerInput.Enable();

        _playerInput.Player.Move.performed += Move;
        _playerInput.Player.Move.canceled += Move;
        _playerInput.Player.GrabDrop.performed += GrabDrop;
        _playerInput.Player.GrabDrop.canceled += GrabDrop;

        _pickupAbility.AccidentalDrop += TogglClawUse;
    }

    private void OnDisable() {
        _playerInput.Player.Move.performed -= Move;
        _playerInput.Player.Move.canceled -= Move;
        _playerInput.Player.GrabDrop.performed -= GrabDrop;
        _playerInput.Player.GrabDrop.canceled -= GrabDrop;

        _pickupAbility.AccidentalDrop -= TogglClawUse;
    }

    private void Move(InputAction.CallbackContext context) {
        Vector2 moveDirection = context.ReadValue<Vector2>();

        _playerMovement.SetMoveDirection(moveDirection);
        _playerAnimator.HandleSpriteFlip(moveDirection);
    }

    private void GrabDrop(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
            TogglClawUse();
        }
    }

    public void TogglClawUse() {
        _isUsingClaw = !_isUsingClaw;

        _playerAnimator.SetClawAnimation(_isUsingClaw);

        if (_isUsingClaw) {
            _pickupAbility.Grab();
        }
        else {
            _pickupAbility.Drop();
        }
    }
}