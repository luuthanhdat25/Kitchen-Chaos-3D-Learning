using System;
using UnityEngine;

namespace Modular.UI
{
    public class DeliveryManagerUI : MonoBehaviour
    {
        [SerializeField] private Transform containerTransform;
        [SerializeField] private Transform templateTransform;

        private void Awake() => templateTransform.gameObject.SetActive(false);

        private void Start()
        {
            SubscribleEvent();
            UpdateVisual();
        }

        private void SubscribleEvent()
        {
            DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
            DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        }

        private void DeliveryManager_OnRecipeSpawned(object sender, EventArgs e)
        {                
            Debug.Log("Recipe");
            UpdateVisual();
        }

        private void DeliveryManager_OnRecipeCompleted(object sender, EventArgs e)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            foreach (Transform childTransform in this.containerTransform)
            {
                if(childTransform != templateTransform) 
                    Destroy(childTransform.gameObject);
            }
            
            foreach (RecipeSO waitingRecipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
            {
                Transform recipeTransfom = Instantiate(templateTransform, this.containerTransform);
                recipeTransfom.gameObject.SetActive(true);
                recipeTransfom.GetComponent<DeliveryManagerSingleUI>().SetRecipeSOName(waitingRecipeSO);
            }
        }
    }
}