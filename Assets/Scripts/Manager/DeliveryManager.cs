using System;
using System.Collections.Generic;
using System.Linq;
using Modular.KitchenObjects;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }
    
    [SerializeField] private RecipeListSO recipeListSo;
    [SerializeField] private List<RecipeSO> waitingRecipeSOList = new List<RecipeSO>();
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;

    private void Awake()
    {
        if(Instance != null) Debug.Log("DeliverManager is already initialized!");
        Instance = this;
    }

    private void Start()
    {
        spawnRecipeTimer = spawnRecipeTimerMax;
    }

    private void FixedUpdate()
    {
        WaitingRecipeSpawn();
    }

    private void WaitingRecipeSpawn()
    {
        spawnRecipeTimer -= Time.fixedDeltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if (waitingRecipeSOList.Count < waitingRecipeMax)
            {
                RecipeSO waitingRecipeSo =
                    recipeListSo.GetRecipeList()[UnityEngine.Random.Range(0, recipeListSo.GetRecipeList().Count)];
                Debug.Log(waitingRecipeSo.GetNameRecipe());
                waitingRecipeSOList.Add(waitingRecipeSo);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSo = waitingRecipeSOList[i];
            
            if (AreRecipeEqual(waitingRecipeSo.GetKitchenObjectList(), 
                                plateKitchenObject.GetKitchenObjectSOList()))
            {
                Debug.Log("Match");
                return;
            }
        }
        Debug.Log("Failed");
    }
    
    public bool AreRecipeEqual (List<KitchenObjectSO> recipe1, List<KitchenObjectSO> recipe2)
    {
        if (recipe1.Count != recipe2.Count) 
            return false;

        HashSet<KitchenObjectSO> set1 = new HashSet<KitchenObjectSO>(recipe1);
        return recipe2.All(set1.Contains);
    }
}
