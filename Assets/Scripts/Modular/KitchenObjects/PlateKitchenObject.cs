using System.Collections.Generic;
using UnityEngine;

namespace Modular.KitchenObjects
{
    public class PlateKitchenObject : KitchenObject
    {
        [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
        private List<KitchenObjectSO> kitchenObjectsSOList;

        private void Awake() => kitchenObjectsSOList = new List<KitchenObjectSO>();

        public bool TryAddIngredient(KitchenObjectSO kitchenObjectSo)
        {
            if (!validKitchenObjectSOList.Contains(kitchenObjectSo)) return false;
            if (kitchenObjectsSOList.Contains(kitchenObjectSo)) return false;
            
            kitchenObjectsSOList.Add(kitchenObjectSo);
            return true;
        }
    }
}