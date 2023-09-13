
using System;

namespace KitchenObjects.Counter
{
    public class TrashCounter : BaseCounter
    {
        public static event EventHandler OnAnyObjectTrashed;
        
        new public static void ResetStaticData() => OnAnyObjectTrashed = null;

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