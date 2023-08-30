using System;
using Modular.Counter;
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
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.GetPrefab());
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(playerInteraction);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}