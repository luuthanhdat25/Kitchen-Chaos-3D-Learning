using System.Collections.Generic;
using UnityEngine;

public class RecipeListSO : ScriptableObject
{
    [SerializeField] private List<RecipeSO> recipeSOList;

    public List<RecipeSO> GetRecipeList() => recipeSOList;
}
