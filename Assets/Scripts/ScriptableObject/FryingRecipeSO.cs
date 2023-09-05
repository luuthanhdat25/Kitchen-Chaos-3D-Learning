using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject
{
    [SerializeField] private KitchenObjectSO input;
    [SerializeField] private KitchenObjectSO output;
    [SerializeField] private float fryingTimerMax;

    public KitchenObjectSO GetInputSO() => input;
    public KitchenObjectSO GetOutputSO() => output;
    public float GetFryingTimerMax() => fryingTimerMax;
}
