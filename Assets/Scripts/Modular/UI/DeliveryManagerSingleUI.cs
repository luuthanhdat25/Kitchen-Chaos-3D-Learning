using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modular.UI
{
    public class DeliveryManagerSingleUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameRecipe;
        [SerializeField] private Transform iconContainer;
        [SerializeField] private Transform iconTemplate;

        private void Start() => iconTemplate.gameObject.SetActive(false);

        public void SetRecipeSOName(RecipeSO recipeSO)
        {
            nameRecipe.text = recipeSO.GetNameRecipe();
            
            foreach (Transform childTransform in this.iconContainer)
            {
                if(childTransform != iconTemplate) 
                    Destroy(childTransform.gameObject);
            }
            
            foreach (KitchenObjectSO kitchenObjectSo in recipeSO.GetKitchenObjectList())
            {
                Transform iconTransfom = Instantiate(iconTemplate, this.iconContainer);
                iconTransfom.gameObject.SetActive(true);
                iconTransfom.GetComponent<Image>().sprite = kitchenObjectSo.GetSprite();
            }
        }
    }
}