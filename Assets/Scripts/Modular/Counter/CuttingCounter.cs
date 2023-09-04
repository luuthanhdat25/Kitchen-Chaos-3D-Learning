using System;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSoArray;
    private int cuttingProcess;

    public event EventHandler OnCut;
    public event EventHandler<OnProgressChangedEventArgs> OnProcessChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }
    
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
                        new OnProgressChangedEventArgs()
                        {
                            progressNormalized = (float)cuttingProcess / cuttingRecipeSo.GetCuttingProgressMax()
                        });
                }
            }
        }else{
            if (!playerInteraction.HasKitchenObject()) {
                GetKitchenObject().SetKitchenObjectParent(playerInteraction);
            }
        }
    }

    private bool IsInputKitChenObjectHasRecipe(KitchenObjectSO inputKitchenObjectSo)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSO(inputKitchenObjectSo);
        if(cuttingRecipeSO != null) return true;
        return false;
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
    
    public override void InteractAlternate(PlayerInteraction playerInteraction) {
        if (HasKitchenObject() && IsInputKitChenObjectHasRecipe(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProcess++;
            CuttingRecipeSO cuttingRecipeSo = GetCuttingRecipeSO(GetKitchenObject().GetKitchenObjectSO());
            
            OnCut?.Invoke(this, EventArgs.Empty);
            OnProcessChanged?.Invoke(this,
                new OnProgressChangedEventArgs()
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


