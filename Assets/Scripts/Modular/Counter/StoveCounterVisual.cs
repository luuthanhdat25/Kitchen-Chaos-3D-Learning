using System;
using UnityEngine;

namespace KitchenObjects.Counter
{
    public class StoveCounterVisual : RepeatMonoBehaviour
    {
        [SerializeField] private StoveCounter stoveCounter;
        [SerializeField] private GameObject stoveOnGameObject;
        [SerializeField] private GameObject particleGameObject;

        private void Start()
        {
            LoadStoveCounterComponent();
            SubscribeOnStateChanged();
        }

        private void SubscribeOnStateChanged()
        {
            stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        }

        private void LoadStoveCounterComponent()
        {
            if (this.stoveCounter != null) return;
            this.stoveCounter = FindComponentInParent<StoveCounter>();
        }

        private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
        {
            bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
            stoveOnGameObject.SetActive(showVisual);
            particleGameObject.SetActive(showVisual);
        }
    }
}