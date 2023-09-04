using System;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

namespace Modular.UI
{
    public class ProgressBarUI : RepeatMonoBehaviour
    {
        [SerializeField] private CuttingCounter cuttingCounter;
        [SerializeField] private Image barImage;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadCuttingCounterComponent();
            LoadBarImageComponent();
        }

        private void LoadCuttingCounterComponent()
        {
            if (this.cuttingCounter != null) return;
            this.cuttingCounter = FindComponentInParent<CuttingCounter>();
        }
        
        private void LoadBarImageComponent()
        {
            if (this.barImage != null) return;
            this.barImage = transform.Find("Bar").GetComponent<Image>();
        }

        private void Start()
        {
            cuttingCounter.OnProcessChanged += CuttingCounter_OnProcessChanged;
            barImage.fillAmount = 0;
            Hide();
        }

        private void CuttingCounter_OnProcessChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
        {
            barImage.fillAmount = e.progressNormalized;
            
            if(e.progressNormalized == 0 || e.progressNormalized == 1) 
                Hide();
            else 
                Show();
        }

        private void Show() => gameObject.SetActive(true);
        private void Hide() => gameObject.SetActive(false);
    }
}