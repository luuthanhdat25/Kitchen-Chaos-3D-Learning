using Modular.KitchenObjects;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] protected Transform counterTopPoint;
    protected KitchenObject kitchenObject;
    
    public virtual void Interact(PlayerInteraction playerInteraction) {
        Debug.LogError("BaseCounter.Interact();");
    }
    
    public virtual void InteractAlternate(PlayerInteraction playerInteraction) {
    }
    
    public void ClearKitchenObjects() => this.kitchenObject = null;

    public bool HasKitchenObject() => this.kitchenObject != null;

    public Transform GetKitchenObjectFollowTransform() => this.counterTopPoint;

    public KitchenObject GetKitchenObject() => this.kitchenObject;

    public void SetKitchenObject(KitchenObject kitchenObject) => this.kitchenObject = kitchenObject;
}
