using System;
using UnityEngine;

namespace KitchenObjects.Counter
{
    public class CuttingCounterAnimation : RepeatMonoBehaviour
    {
        [SerializeField] private CuttingCounter cuttingCounter;
        [SerializeField] private Animator animator;
        private const string CUT = "Cut";
        
        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadCuttingCounterComponent();
            LoadAnimatorComponent();
        }

        private void LoadCuttingCounterComponent()
        {
            if (this.cuttingCounter != null) return;
            this.cuttingCounter = FindComponentInParent<CuttingCounter>();
        }
        
        private void LoadAnimatorComponent()
        {
            if (this.animator != null) return;
            this.animator = GetComponent<Animator>();
        }

        private void Start() => cuttingCounter.OnCut += CuttingCounter_OnCut;

        private void CuttingCounter_OnCut(object sender, EventArgs e)
        {
            this.animator.SetTrigger(CUT);            
        }
    }
}