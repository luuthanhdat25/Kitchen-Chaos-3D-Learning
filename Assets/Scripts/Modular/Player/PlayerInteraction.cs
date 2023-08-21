using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounterInteracted SelectedCounterInteracted;
    }
    
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private LayerMask counterLayerMask;
        private ClearCounterInteracted selectedCounterInteracted;
        private Vector3 lastInteractDir;
        private const float INTERACT_DISTANCE = 2f;
        private const float RAYCAST_PADDING_BOTTOM = 1f;
        
        public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
        
        private void Start()
        {
            SubscribeOnInteract();
        }

        private void SubscribeOnInteract() => InputManager.Instance.OnInteract += GameInputOnInteract;

        private void GameInputOnInteract(object sender, EventArgs e)
        {
            if (selectedCounterInteracted != null) 
                selectedCounterInteracted.Interact();
        }

        private void Update() => HandleInteractions();

        private void HandleInteractions()
        {
            Vector2 inputVector = GetMovementVectorNormalized();
            Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
            //If player stop still can dectect the Counter
            if (moveDirection != Vector3.zero) lastInteractDir = moveDirection;
            
            Interaction();
        }

        private void Interaction()
        {
            if (IsHasCounter(out var raycastHit))
            {
                if (raycastHit.transform.TryGetComponent(out ClearCounterInteracted clearCounterInteracted))
                {
                    clearCounterInteracted.Interact();
                    //if (clearCounterInteracted != selectedCounterInteracted) SetSelectedCounter(clearCounterInteracted);
                }
                //else SetSelectedCounter(null);
            }
            //else SetSelectedCounter(null);
        }

        private bool IsHasCounter(out RaycastHit raycastHit)
        {
            return Physics.Raycast(OriginRayCast(), 
                            lastInteractDir, 
                                   out raycastHit, 
                                   INTERACT_DISTANCE,
                                counterLayerMask);
        }

        private Vector3 OriginRayCast()
        {
            Vector3 origin = transform.parent.position;
            origin.y += RAYCAST_PADDING_BOTTOM;
            return origin;
        }
        
        private Vector2 GetMovementVectorNormalized() => InputManager.Instance.GetMovementVectorNormalized();

        private void SetSelectedCounter(ClearCounterInteracted clearCounterInteracted)
        {
            this.selectedCounterInteracted = clearCounterInteracted;
        
            OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
            {
                SelectedCounterInteracted = this.selectedCounterInteracted
            });
        }
    }
}