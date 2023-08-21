using DefaultNamespace;
using UnityEngine;

public class ClearCounterInteracted : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;
    public void Interact()
    {
        Debug.Log("Interact");
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
