using KitchenObjects.Counter;
using UnityEngine;
using UnityEngine.UI;

namespace Modular.UI
{
    public class ProgressBarUI : MonoBehaviour
    {
        [SerializeField] private GameObject hasProgressGameObject;
        [SerializeField] private Image barImage;
        
        private IHasProgess hasProgess;
        
        private void Start()
        {
            hasProgess = hasProgressGameObject.GetComponent<IHasProgess>();
            if (hasProgess == null)
                Debug.LogError(
                    $"GameObject: {hasProgressGameObject.name} doesn't have a component has implements IHasProgess");
            
            hasProgess.OnProcessChanged += IHasProgress_OnProcessChanged;
            barImage.fillAmount = 0;
            Hide();
        }

        private void IHasProgress_OnProcessChanged(object sender, IHasProgess.OnProgressChangedEventArgs e)
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