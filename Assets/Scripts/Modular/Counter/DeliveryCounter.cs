using Modular.KitchenObjects;
using UnityEngine;

namespace KitchenObjects.Counter
{
    public class DeliveryCounter : BaseCounter
    {
        public static DeliveryCounter Instance { get; private set; }

        private void Awake()
        {
            if(Instance != null) Debug.LogError("DeliveryCounter already exists");
            Instance = this;
        }

        public override void Interact(PlayerInteraction playerInteraction)
        {
            if (playerInteraction.HasKitchenObject())
            {
                //Only accept plate items
                if (playerInteraction.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                    playerInteraction.GetKitchenObject().DestroyItSelf();
                }
            }
        }
    }
}