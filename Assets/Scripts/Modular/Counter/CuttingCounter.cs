using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO cuttingKitObjectSO;
    
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
    
    public override void InteractAlternate(PlayerInteraction playerInteraction) {
        if (HasKitchenObject())
        {
            GetKitchenObject().DestroyItSelf();
            KitchenObject.SpawnKitchenObject(cuttingKitObjectSO, this);
        }
    }
}


