using Modular.KitchenObjects;
using System;
using Modular.Counter;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform kitchenObjectHoldingPointTransform;
    private BaseCounter selectedCounter;
    private Vector3 lastInteractDir;
    private KitchenObject kitchenObject;
    
    private const float INTERACT_DISTANCE = 2f;
    private const float RAYCAST_PADDING_BOTTOM = 1f;
    
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }
    
    private void Start() => SubscribeOnInteract();

    private void SubscribeOnInteract() => InputManager.Instance.OnInteract += GameInputOnInteract;

    private void GameInputOnInteract(object sender, EventArgs e)
    {
        if (selectedCounter != null) 
            selectedCounter.Interact(this);
    }

    private void Update() => HandleInteractions();

    private void HandleInteractions()
    {
        Vector3 moveDirection = GetMoveDirection();
        
        //Using lastDirection to Interaction
        if (moveDirection != Vector3.zero) 
            lastInteractDir = moveDirection;
        Interaction();
    }

    private Vector3 GetMoveDirection()
    {
        Vector2 inputVector = GetMovementVectorNormalized();
        return Vector3.forward*inputVector.y + Vector3.right*inputVector.x;
    }

    private Vector2 GetMovementVectorNormalized() => InputManager.Instance.GetMovementVectorNormalized();

    private void Interaction()
    {
        if (IsHasCounter(out RaycastHit raycastHit))
        {
            if (raycastHit.transform.parent.TryGetComponent(out BaseCounter counterInteracted))
            {
                if (counterInteracted != selectedCounter) 
                    SetSelectedCounter(counterInteracted);
            }
            else SetSelectedCounter(null);
        }
        else SetSelectedCounter(null);
    }

    private bool IsHasCounter(out RaycastHit raycastHit)
    {
        return Physics.Raycast(GetOriginRayCast(), 
                        lastInteractDir, 
                               out raycastHit, 
                               INTERACT_DISTANCE,
                            counterLayerMask);
    }

    private Vector3 GetOriginRayCast()
    {
        Vector3 origin = transform.parent.position;
        origin.y += RAYCAST_PADDING_BOTTOM;
        return origin;
    }
    
    private void SetSelectedCounter(BaseCounter counterInteracted)
    {
        this.selectedCounter = counterInteracted;
    
        OnSelectedCounterChanged?.Invoke(this, 
                new OnSelectedCounterChangedEventArgs{
                    selectedCounter = this.selectedCounter 
                });
    }

    public Transform GetKitchenObjectFollowTransform() => this.kitchenObjectHoldingPointTransform;

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() => this.kitchenObject;

    public void ClearKitchenObjects() => this.kitchenObject = null;

    public bool HasKitchenObject() => this.kitchenObject != null;
}
