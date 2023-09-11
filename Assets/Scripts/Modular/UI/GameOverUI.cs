using System;
using TMPro;
using UnityEngine;

namespace Modular.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI recipeDeliveriedText;
        
        private void Start()
        {
            GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
            Show(false);
        }

        private void GameManager_OnStateChanged(object sender, EventArgs e)
        {
            if (GameManager.Instance.IsGameOver())
            {
                Show(true);
                recipeDeliveriedText.text = DeliveryManager.Instance.GetSuccessfulRecipeDeliveriedAmount().ToString();
            }
            else Show(false);
        }

        private void Show(bool isShow) => gameObject.SetActive(isShow);
    }
}