using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
    [SerializeField] private List<KitchenObjectSO> kitchenObjectSOList;
    [SerializeField] private string nameRecipe;

    public List<KitchenObjectSO> GetKitchenObjectList() => kitchenObjectSOList;
    public string GetNameRecipe() => nameRecipe;
}
