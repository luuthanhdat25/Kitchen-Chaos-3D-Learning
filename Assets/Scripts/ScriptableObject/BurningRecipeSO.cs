using UnityEngine;

[CreateAssetMenu()]
public class BurningRecipeSO : ScriptableObject
{
    [SerializeField] private KitchenObjectSO input;
    [SerializeField] private KitchenObjectSO output;
    [SerializeField] private float burningTimerMax;

    public KitchenObjectSO GetInputSO() => input;
    public KitchenObjectSO GetOutputSO() => output;
    public float GetBurningTimerMax() => burningTimerMax;
}
