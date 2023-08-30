using KitchenObjects;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private ClearCounter secondaryCounter;
    private KitchenObject kitchenObject;
    
    public void Interact(PlayerInteraction playerInteraction)
    {
        if (this.kitchenObject != null)
        {
            this.kitchenObject.SetKitchenObjectParent(playerInteraction);
            return;
        }
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.Prefab, counterTopPoint);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
    }
    
    public void ClearKitchenObjects() => this.kitchenObject = null;
    
    public bool HasKitchenObject() => this.kitchenObject != null;
    
    public Transform GetKitchenObjectFollowTransform() => this.counterTopPoint;
    
    public KitchenObject GetKitchenObject() => this.kitchenObject;
    
    public void SetKitchenObject(KitchenObject kitchenObject) => this.kitchenObject = kitchenObject;
}
