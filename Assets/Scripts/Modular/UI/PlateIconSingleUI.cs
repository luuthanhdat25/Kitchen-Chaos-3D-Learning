using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour
{
    [SerializeField] private Image icon;

    public void SetKitchenObjectSOIcon(KitchenObjectSO kitchenObjectSo)
    {
        icon.sprite = kitchenObjectSo.GetSprite();
    }
}
