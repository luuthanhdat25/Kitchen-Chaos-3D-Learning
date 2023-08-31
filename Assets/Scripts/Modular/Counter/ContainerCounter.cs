using System;
using UnityEngine;

namespace KitchenObjects.Counter
{
    public class ContainerCounter: BaseCounter
    {
        public event EventHandler OnPlayerGrabbedObject;
        
        [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
        public override void Interact(PlayerInteraction playerInteraction)
        {
            if (playerInteraction.HasKitchenObject()) return;
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, playerInteraction);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}