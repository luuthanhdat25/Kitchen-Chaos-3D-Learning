using System;
using System.Collections.Generic;
using Modular.KitchenObjects;
using UnityEngine;

namespace Modular.Others
{
    public class PlateCompleteVisual : MonoBehaviour
    {
        [Serializable]
        public struct KitchenObjectSO_GameObject
        {
            public KitchenObjectSO kitchenObjectSo;
            public GameObject gameObject;
        }
        
        [SerializeField] private PlateKitchenObject plateKitchenObject;
        [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;

        private void Start()
        {
            plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

            foreach (KitchenObjectSO_GameObject kitchenObjectSoGameObject in kitchenObjectSOGameObjectList)
                kitchenObjectSoGameObject.gameObject.SetActive(false);
        }

        private void PlateKitchenObject_OnIngredientAdded(object sender,
            PlateKitchenObject.OnIngredientAddedEventArgs e)
        {
            foreach (KitchenObjectSO_GameObject kitchenObjectSoGameObject in kitchenObjectSOGameObjectList)
                if(e.kitchenObjectSo == kitchenObjectSoGameObject.kitchenObjectSo)
                    kitchenObjectSoGameObject.gameObject.SetActive(true);
        }
    }
}