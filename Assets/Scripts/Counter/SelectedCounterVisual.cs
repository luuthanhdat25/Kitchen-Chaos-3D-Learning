using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;
    
    private void Start() => SubscribeOnSelectedCounterChanged();

    private void SubscribeOnSelectedCounterChanged()
    {
        PlayerController.Instance.PlayerInteraction.OnSelectedCounterChanged
            += OnSelectedCounterChanged;
    }

    private void OnSelectedCounterChanged(object sender, 
        PlayerInteraction.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == clearCounter) 
            ShowVisual(true);
        else 
            ShowVisual(false);
    }

    private void ShowVisual(bool isOn) => visualGameObject.SetActive(isOn);
}
