using UnityEngine;

namespace DefaultNamespace
{
    public class KitchenObject : MonoBehaviour
    {
        [SerializeField] private KitchenObjectSO kitchenObjectSO;

        private ClearCounterInteracted clearCounterInteracted;

        public KitchenObjectSO GetKitchenObjectSO()
        {
            return kitchenObjectSO;
        }
        

        public void SetClearCounter(ClearCounterInteracted clearCounterInteracted)
        {
            this.clearCounterInteracted = clearCounterInteracted;
        }

        public ClearCounterInteracted GetClearCounter()
        {
            return clearCounterInteracted;
        }
    }
}