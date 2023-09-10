using System;
using TMPro;
using UnityEngine;

namespace Modular.UI
{
    public class GameStartCountDownUI : MonoBehaviour
    {
        private TextMeshProUGUI countdownText;

        private void Awake() => countdownText = GetComponent<TextMeshProUGUI>();

        private void Start()
        {
            GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
            Show(false);
        }

        private void GameManager_OnStateChanged(object sender, EventArgs e)
        {
            if (GameManager.Instance.IsCountDownToStartIsActive())
            {
                Show(true);
            }
            else
            {
                Show(false);
            }
        }

        private void Show(bool isShow)
        {
            gameObject.SetActive(isShow);
        }

        private void Update()
        {
            countdownText.text = Mathf.Ceil(GameManager.Instance.GetCoundownToStartTimer()).ToString();
        }
    }
}