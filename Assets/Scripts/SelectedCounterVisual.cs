using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class SelectedCounterVisual : MonoBehaviour
    {
        [FormerlySerializedAs("clearCounterInteract")] [FormerlySerializedAs("clearCounter")] [SerializeField] private ClearCounterInteracted clearCounterInteracted;
        [SerializeField] private GameObject visualGameObject;
        
        // private void Start()
        // {
        //     PlayerMovement.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
        // }
        //
        // private void Player_OnSelectedCounterChanged(object sender, PlayerMovement.OnSelectedCounterChangedEventArgs e)
        // {
        //     if (e.selectedCounter == clearCounter) Show();
        //     else Hide();
        // }

        private void Show()
        {
            visualGameObject.SetActive(true);
        }
        
        private void Hide()
        {
            visualGameObject.SetActive(false);
        }
    }
}