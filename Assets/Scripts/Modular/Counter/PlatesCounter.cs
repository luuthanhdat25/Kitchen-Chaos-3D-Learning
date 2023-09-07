using System;
using UnityEngine;

namespace KitchenObjects.Counter
{
    public class PlatesCounter : BaseCounter
    {
        public event EventHandler OnPlateSpawned;
        public event EventHandler OnPlateRemoved;

        [SerializeField] private KitchenObjectSO platesKitchenObjectSo;
        [SerializeField] private float spawnPlatesTimerMax = 4f;
        [SerializeField] private int platesCountMax = 4;
        private int platesCount;
        private float spawnPlateTimer;

        private void FixedUpdate()
        {
            spawnPlateTimer += Time.fixedDeltaTime;
            if (spawnPlateTimer >= spawnPlatesTimerMax)
            {
                spawnPlateTimer = 0;
                if (platesCount < platesCountMax)
                {
                    platesCount++;
                    OnPlateSpawned?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public override void Interact(PlayerInteraction playerInteraction)
        {
            if (!playerInteraction.HasKitchenObject() && platesCount > 0)
            {
                platesCount--;
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
                KitchenObject.SpawnKitchenObject(platesKitchenObjectSo, playerInteraction);
            }
        }
    }
}