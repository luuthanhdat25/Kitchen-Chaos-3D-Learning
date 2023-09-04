using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject
{
    [SerializeField] private KitchenObjectSO input;
    [SerializeField] private KitchenObjectSO output;
    [SerializeField] private int cuttingProgressMax;

    public KitchenObjectSO GetInputSO() => input;
    public KitchenObjectSO GetOutputSO() => output;
    public int GetCuttingProgressMax() => cuttingProgressMax;
}
