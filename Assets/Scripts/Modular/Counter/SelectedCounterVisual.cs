using Modular.Counter;
using UnityEngine;

public class SelectedCounterVisual : RepeatMonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject visualGameObject;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBaseCounterComponent();
    }

    private void LoadBaseCounterComponent()
    {
        if(this.baseCounter != null) return;
        baseCounter = FindComponentInParent<BaseCounter>();
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
        if (e.selectedCounter == baseCounter) 
            ShowVisual(true);
        else 
            ShowVisual(false);
    }

    private void ShowVisual(bool isOn) => visualGameObject.SetActive(isOn);
}
