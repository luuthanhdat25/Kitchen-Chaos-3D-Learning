using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
     private KitchenObject kitchenObject;
    
    public void Interact()
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.Prefab, counterTopPoint);
        kitchenObjectTransform.localPosition = Vector3.zero;
        
        // if (kitchenObject == null)
        // {
        //     Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
        //     kitchenObjectTransform.localPosition = Vector3.zero;
        //
        //     kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        //     kitchenObject.SetClearCounter(this);
        // }
        // else
        // {
        //     Debug.Log(kitchenObject.GetClearCounter());
        // }
    }
}
