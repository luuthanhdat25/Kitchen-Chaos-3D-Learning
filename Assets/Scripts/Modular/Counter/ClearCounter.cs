using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    public override void Interact(PlayerInteraction playerInteraction)
    {
        if (!HasKitchenObject())
        {
            if (playerInteraction.HasKitchenObject())
            {
                playerInteraction.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {
            if (!playerInteraction.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(playerInteraction);
            }
        }
    }
}
