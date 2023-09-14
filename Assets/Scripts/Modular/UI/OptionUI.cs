using System;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modular.UI
{
    public class OptionUI : MonoBehaviour
    {
        public static OptionUI Instance { get; private set; }
        
        [SerializeField] private Button soundEffectButton;
        [SerializeField] private TextMeshProUGUI soundEffectVolumeText;
        [SerializeField] private Button musicButton;
        [SerializeField] private TextMeshProUGUI musicVolumeText;
        [SerializeField] private Button closeButton;

        private void Awake()
        {
            if(Instance != null) Debug.LogError("OptionUI is already initialized");
            Instance = this;
            
            soundEffectButton.onClick.AddListener(() =>
            {
                SoundManager.Instance.ChangeVolume();
                UpdateVisual();
            });
            musicButton.onClick.AddListener(() =>
            {
                MusicManager.Instance.ChangeVolume();
                UpdateVisual();
            });
            closeButton.onClick.AddListener(() =>
            {
                Show(false);
            });
        }

        private void UpdateVisual()
        {
            soundEffectVolumeText.text = $"Sound Effect: {Mathf.Round(SoundManager.Instance.GetVolume()*10)}";
            musicVolumeText.text = $"Music: {Mathf.Round(MusicManager.Instance.GetVolume()*10)}";
        }
        
        private void Start()
        {
            GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
            UpdateVisual();
            Show(false);
        }

        private void GameManager_OnGameUnpaused(object sender, EventArgs e) 
            => Show(false);

        public void Show(bool isShow) => gameObject.SetActive(isShow);
    }
}