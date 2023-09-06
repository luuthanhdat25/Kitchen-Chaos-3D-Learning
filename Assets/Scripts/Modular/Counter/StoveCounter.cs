using System;
using Modular.KitchenObjects;
using UnityEngine;

namespace KitchenObjects.Counter
{
    public class StoveCounter : BaseCounter, IHasProgess
    {
        public enum State
        {
            Idle,
            Frying,
            Fried,
            Burned
        }
        
        [SerializeField] private FryingRecipeSO[] fryingRecipeSoArray;
        [SerializeField] private BurningRecipeSO[] burningRecipeSoArray;
        
        private State state;
        private FryingRecipeSO fryingRecipeSo;
        private BurningRecipeSO burningRecipeSo;
        private float fryingTimer;
        private float burningTimer;

        public event EventHandler<IHasProgess.OnProgressChangedEventArgs> OnProcessChanged;
        public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
        public class OnStateChangedEventArgs : EventArgs
        {
            public State state;
        }
        
        private void Start() => state = State.Idle;

        private void FixedUpdate()
        {
            if (!HasKitchenObject()) return;
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.fixedDeltaTime;
                    
                    OnProcessChanged?.Invoke(this, new IHasProgess.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeSo.GetFryingTimerMax()
                    });
                    
                    if (fryingTimer > fryingRecipeSo.GetFryingTimerMax())
                    {
                        GetKitchenObject().DestroyItSelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSo.GetOutputSO(), this);
                        ChangeStateAndInvokeOnStateChanged(State.Fried);

                        burningTimer = 0;
                        burningRecipeSo = GetBurningRecipeSO(GetKitchenObject().GetKitchenObjectSO());
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.fixedDeltaTime;
                    
                    OnProcessChanged?.Invoke(this, new IHasProgess.OnProgressChangedEventArgs
                    {
                        progressNormalized = burningTimer / burningRecipeSo.GetBurningTimerMax()
                    });
                    
                    if (burningTimer > burningRecipeSo.GetBurningTimerMax())
                    {
                        GetKitchenObject().DestroyItSelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSo.GetOutputSO(), this);
                        ChangeStateAndInvokeOnStateChanged(State.Burned);
                        
                        OnProcessChanged?.Invoke(this, new IHasProgess.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0
                        });
                    }
                    break;
                case State.Burned:
                    break;
            }
            Debug.Log(state);
        }

        private void ChangeStateAndInvokeOnStateChanged(State newState)
        {
            this.state = newState;
            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
            {
                state = newState
            });
        }

        public override void Interact(PlayerInteraction playerInteraction)
        {
            if (!HasKitchenObject()) {
                if (playerInteraction.HasKitchenObject()){
                    if (IsInputKitChenObjectHasRecipe(playerInteraction.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        playerInteraction.GetKitchenObject().SetKitchenObjectParent(this);
                        
                        fryingRecipeSo = GetFryingRecipeSO(GetKitchenObject().GetKitchenObjectSO());
                        
                        fryingTimer = 0;
                        ChangeStateAndInvokeOnStateChanged(State.Frying);
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
                            ChangeStateAndInvokeOnStateChanged(State.Idle);
                            OnProcessChanged?.Invoke(this, new IHasProgess.OnProgressChangedEventArgs
                            {
                                progressNormalized = 0
                            });
                        }
                    }
                }
                else 
                {
                    GetKitchenObject().SetKitchenObjectParent(playerInteraction);
                    ChangeStateAndInvokeOnStateChanged(State.Idle);
                    OnProcessChanged?.Invoke(this, new IHasProgess.OnProgressChangedEventArgs
                    {
                        progressNormalized = 0
                    });
                }
            }
        }
        
        private bool IsInputKitChenObjectHasRecipe(KitchenObjectSO inputKitchenObjectSo)
        {
            FryingRecipeSO fryingRecipeSO = GetFryingRecipeSO(inputKitchenObjectSo);
            return fryingRecipeSO != null;
        }
        
        private FryingRecipeSO GetFryingRecipeSO(KitchenObjectSO inputKitchenObjectSO)
        {
            foreach(FryingRecipeSO fryingRecipeSO in fryingRecipeSoArray)
            {
                if(fryingRecipeSO.GetInputSO() == inputKitchenObjectSO) 
                    return fryingRecipeSO;
            }
            return null;
        }
        
        private BurningRecipeSO GetBurningRecipeSO(KitchenObjectSO inputKitchenObjectSO)
        {
            foreach(BurningRecipeSO burningRecipeSo in burningRecipeSoArray)
            {
                if(burningRecipeSo.GetInputSO() == inputKitchenObjectSO) 
                    return burningRecipeSo;
            }
            return null;
        }
    }
}