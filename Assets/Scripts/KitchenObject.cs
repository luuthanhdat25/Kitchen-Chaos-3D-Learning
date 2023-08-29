using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public KitchenObjectSO KitchenObjectSO => kitchenObjectSO;

    private ClearCounter clearCounter;
    public ClearCounter ClearCounter
    {
        get => clearCounter;
        set => clearCounter = value;
    }
}
