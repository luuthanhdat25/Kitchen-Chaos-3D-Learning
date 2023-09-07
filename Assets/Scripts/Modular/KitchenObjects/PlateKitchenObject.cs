using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modular.KitchenObjects
{
    public class PlateKitchenObject : KitchenObject
    {
        public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
        public class OnIngredientAddedEventArgs : EventArgs
        {
            public KitchenObjectSO kitchenObjectSo;
        }
        
        [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
        private List<KitchenObjectSO> kitchenObjectsSOList;

        private void Awake() => kitchenObjectsSOList = new List<KitchenObjectSO>();

        public bool TryAddIngredient(KitchenObjectSO kitchenObjectSo)
        {
            if (!validKitchenObjectSOList.Contains(kitchenObjectSo)) return false;
            if (kitchenObjectsSOList.Contains(kitchenObjectSo)) return false;
            
            kitchenObjectsSOList.Add(kitchenObjectSo);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                kitchenObjectSo = kitchenObjectSo
            });
            return true;
        }

        public List<KitchenObjectSO> GetKitchenObjectSOList() => this.kitchenObjectsSOList;
    }
}