using UnityEngine;

namespace KitchenObjects.Counter
{
    public class TrashCounter : BaseCounter
    {
        public override void Interact(PlayerInteraction playerInteraction)
        {
            if (playerInteraction.HasKitchenObject())
                playerInteraction.GetKitchenObject().DestroyItSelf();
        }
    }
}