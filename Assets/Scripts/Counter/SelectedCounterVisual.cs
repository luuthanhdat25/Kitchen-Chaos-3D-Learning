using UnityEngine;

public class SelectedCounterVisual : RepeatMonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadClearCounterComponent();
    }

    private void LoadClearCounterComponent()
    {
        if(this.clearCounter != null) return;
        clearCounter = FindComponentInParent<ClearCounter>();
    }

    private void Start() => SubscribeOnSelectedCounterChanged();

    private void SubscribeOnSelectedCounterChanged()
    {
        PlayerController.Instance.GetPlayerInteraction().OnSelectedCounterChanged
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
