using Modular.KitchenObjects;
using UnityEngine;

namespace Modular.UI
{
    public class PlateIconUI : MonoBehaviour
    {
        [SerializeField] private PlateKitchenObject plateKitchenObject;
        [SerializeField] private Transform iconTemplateTransform;

        private void Awake() => iconTemplateTransform.gameObject.SetActive(false);

        private void Start() => plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

        private void PlateKitchenObject_OnIngredientAdded(object sender,
            PlateKitchenObject.OnIngredientAddedEventArgs e)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            foreach (Transform childTransform in this.transform)
            {
                if(childTransform != iconTemplateTransform) 
                    Destroy(childTransform.gameObject);
            }
            
            foreach (KitchenObjectSO kitchenObjectSo in plateKitchenObject.GetKitchenObjectSOList())
            {
                Transform iconTransfom = Instantiate(iconTemplateTransform, this.transform);
                iconTransfom.gameObject.SetActive(true);
                iconTransfom.GetComponent<PlateIconSingleUI>().SetKitchenObjectSOIcon(kitchenObjectSo);
            }
        }
    }
}