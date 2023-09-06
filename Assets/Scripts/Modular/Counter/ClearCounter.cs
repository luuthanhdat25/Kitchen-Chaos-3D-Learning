using Modular.KitchenObjects;

public class ClearCounter : BaseCounter
{
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
            if (playerInteraction.HasKitchenObject())
            {
                if (playerInteraction.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) 
                    {
                        GetKitchenObject().DestroyItSelf();
                    }
                }
                else
                {
                    if (GetKitchenObject().TryGetPlate(out  plateKitchenObject))
                    {
                        if(plateKitchenObject.TryAddIngredient(
                               playerInteraction.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            playerInteraction.GetKitchenObject().DestroyItSelf();
                        }
                    }
                }
            }
            else 
            {
                GetKitchenObject().SetKitchenObjectParent(playerInteraction);
            }
        }
    }
}
