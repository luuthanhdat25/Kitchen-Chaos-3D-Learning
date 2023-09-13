using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    
    private PlayerInputActions playerInputActions;
    
    private void Awake()
    {
        CheckingSingleton();
        EnableInputActions();
        SubscribeEventPerformed();
    }

    private void CheckingSingleton()
    {
        if (Instance != null) Debug.LogError("There must be only one GameInput exist!");
        Instance = this;
    }
    
    private void EnableInputActions()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }
    
    private void SubscribeEventPerformed()
    {
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;
        
        playerInputActions.Dispose();
    }
    
    private void Interact_performed(InputAction.CallbackContext obj) 
        => OnInteractAction?.Invoke(this, EventArgs.Empty);

    private void InteractAlternate_performed(InputAction.CallbackContext obj) 
        => OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);

    private void Pause_performed(InputAction.CallbackContext obj)
        => OnPauseAction?.Invoke(this, EventArgs.Empty);
    
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }
}
