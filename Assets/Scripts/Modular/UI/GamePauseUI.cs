using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace Modular.UI
{
    public class GamePauseUI : MonoBehaviour
    {
        [SerializeField] private Button continueButton;
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button optionsButton;

        private void Awake()
        {
            continueButton.onClick.AddListener(() =>
            {
                GameManager.Instance.TogglePauseGame();
            });
            mainMenuButton.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene.MainMenuScene);
            });
            optionsButton.onClick.AddListener(() =>
            {
                OptionUI.Instance.ShowOptionUI(true);
            });
        }

        private void Start()
        {
            GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
            GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
            Show(false);
        }

        private void GameManager_OnGamePaused(object sender, EventArgs e) => Show(true);

        private void GameManager_OnGameUnpaused(object sender, EventArgs e) => Show(false);

        private void Show(bool isPause) => gameObject.SetActive(isPause);
    }
}