using System;
using KitchenObjects.Counter;
using Modular.KitchenObjects;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgess
{
    public static event EventHandler OnAnyCut;

    new public static void ResetStaticData() => OnAnyCut = null;
    
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSoArray;
    private int cuttingProcess;

    public event EventHandler<IHasProgess.OnProgressChangedEventArgs> OnProcessChanged;
    public event EventHandler OnCut;
    
    public override void Interact(PlayerInteraction playerInteraction)
    {
        if (!HasKitchenObject()) {
            if (playerInteraction.HasKitchenObject()){
                if (IsInputKitChenObjectHasRecipe(playerInteraction.GetKitchenObject().GetKitchenObjectSO()))
                {
                    playerInteraction.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProcess = 0;

                    CuttingRecipeSO cuttingRecipeSo = GetCuttingRecipeSO(GetKitchenObject().GetKitchenObjectSO());
                    OnProcessChanged?.Invoke(this,
                        new IHasProgess.OnProgressChangedEventArgs
                        {
                            progressNormalized = (float)cuttingProcess / cuttingRecipeSo.GetCuttingProgressMax()
                        });
                }
            }
        }else{
            if (playerInteraction.HasKitchenObject()) 
            {
                if (playerInteraction.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroyItSelf();
                    }
                }    
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(playerInteraction);
            }
        }
    }

    private bool IsInputKitChenObjectHasRecipe(KitchenObjectSO inputKitchenObjectSo)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSO(inputKitchenObjectSo);
        return cuttingRecipeSO != null;
    }
    
    private CuttingRecipeSO GetCuttingRecipeSO(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSoArray)
        {
            if(cuttingRecipeSO.GetInputSO() == inputKitchenObjectSO) 
                return cuttingRecipeSO;
        }
        return null;
    }
    
    //Cutting Kitchen object
    public override void InteractAlternate(PlayerInteraction playerInteraction) {
        if (HasKitchenObject() && IsInputKitChenObjectHasRecipe(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProcess++;
            CuttingRecipeSO cuttingRecipeSo = GetCuttingRecipeSO(GetKitchenObject().GetKitchenObjectSO());
            
            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);
            OnProcessChanged?.Invoke(this,
                new IHasProgess.OnProgressChangedEventArgs()
                {
                    progressNormalized = (float)cuttingProcess / cuttingRecipeSo.GetCuttingProgressMax()
                });
            
            if (cuttingProcess >= cuttingRecipeSo.GetCuttingProgressMax())
            {
                KitchenObjectSO outputKitchenObjectSo = GetOutputKitChenObject(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroyItSelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSo, this);
            }
        }
    }

    private KitchenObjectSO GetOutputKitChenObject(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSO(inputKitchenObjectSO);
        if(cuttingRecipeSO != null) 
            return cuttingRecipeSO.GetOutputSO();
        
        return null;
    }
}


