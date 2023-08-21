using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }
        
        public event EventHandler OnInteract;
        private PlayerInputActions playerInputActions;
        
        private void Awake()
        {
            CheckingSingleton();
            EnableInputActions();
            SubscribeInteractPerformed();
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
        
        private void SubscribeInteractPerformed()
        {
            playerInputActions.Player.Interact.performed += Interact_performed;
        }
        
        private void Interact_performed(InputAction.CallbackContext obj)
        {
            OnInteract?.Invoke(this, EventArgs.Empty);
        }

        public Vector2 GetMovementVectorNormalized()
        {
            Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
            return inputVector.normalized;
        }
    }
}