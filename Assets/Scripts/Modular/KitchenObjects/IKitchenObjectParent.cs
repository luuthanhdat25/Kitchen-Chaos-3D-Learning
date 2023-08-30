using UnityEngine;

namespace Modular.KitchenObjects
{
    public interface IKitchenObjectParent
    {
        public Transform GetKitchenObjectFollowTransform();
        
        public void SetKitchenObject(KitchenObject kitchenObject);
        
        public KitchenObject GetKitchenObject();
        
        public void ClearKitchenObjects();
        
        public bool HasKitchenObject();
    }
}