using UnityEngine;
using UnityEngine.UI;

namespace Modular.UI
{
    public class GamePlayClockUI : MonoBehaviour
    {
        [SerializeField] private Image clockCircleImage;
       
        private void Update()
        {
            if (!GameManager.Instance.IsGamePlaying()) return;
            clockCircleImage.fillAmount = GameManager.Instance.GetCoundownToGamePlayTimerNormalized();
        }
    }
}