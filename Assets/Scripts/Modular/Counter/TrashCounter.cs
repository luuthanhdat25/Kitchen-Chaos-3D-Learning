
using System;

namespace KitchenObjects.Counter
{
    public class TrashCounter : BaseCounter
    {
        public static event EventHandler OnAnyObjectTrashed;
        
        public override void Interact(PlayerInteraction playerInteraction)
        {
            if (playerInteraction.HasKitchenObject())
            {
                playerInteraction.GetKitchenObject().DestroyItSelf();
                OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}