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
        [SerializeField] private Button moveUpButton;
        [SerializeField] private Button moveDownButton;
        [SerializeField] private Button moveLeftButton;
        [SerializeField] private Button moveRightButton;
        [SerializeField] private Button interactButton;
        [SerializeField] private Button interactAltButton;
        [SerializeField] private Button pauseGameButton;
        [SerializeField] private TextMeshProUGUI moveUpText;
        [SerializeField] private TextMeshProUGUI moveDownText;
        [SerializeField] private TextMeshProUGUI moveLeftText;
        [SerializeField] private TextMeshProUGUI moveRightText;
        [SerializeField] private TextMeshProUGUI interactText;
        [SerializeField] private TextMeshProUGUI interactAltText;
        [SerializeField] private TextMeshProUGUI pauseGameText;
        [SerializeField] private Transform pressToRebindKeyTransform;


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
                ShowOptionUI(false);
            });
            
            moveUpButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Move_Up);});
            moveDownButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Move_Down);});
            moveLeftButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Move_Left);});
            moveRightButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Move_Right);});
            interactButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Interact);});
            interactAltButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.InteractAlternate);});
            pauseGameButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Pause);});
        }

        private void UpdateVisual()
        {
            soundEffectVolumeText.text = $"Sound Effect: {Mathf.Round(SoundManager.Instance.GetVolume()*10)}";
            musicVolumeText.text = $"Music: {Mathf.Round(MusicManager.Instance.GetVolume()*10)}";

            moveUpText.text = InputManager.Instance.GetBidingText(InputManager.Binding.Move_Up);
            moveDownText.text = InputManager.Instance.GetBidingText(InputManager.Binding.Move_Down);
            moveLeftText.text = InputManager.Instance.GetBidingText(InputManager.Binding.Move_Left);
            moveRightText.text = InputManager.Instance.GetBidingText(InputManager.Binding.Move_Right);
            interactText.text = InputManager.Instance.GetBidingText(InputManager.Binding.Interact);
            interactAltText.text = InputManager.Instance.GetBidingText(InputManager.Binding.InteractAlternate);
            pauseGameText.text = InputManager.Instance.GetBidingText(InputManager.Binding.Pause);
        }
        
        private void Start()
        {
            GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
            UpdateVisual();
            ShowOptionUI(false);
            HidePressToRebindKey();
        }

        private void GameManager_OnGameUnpaused(object sender, EventArgs e) 
            => ShowOptionUI(false);

        public void ShowOptionUI(bool isShow) => gameObject.SetActive(isShow);
        
        public void ShowPressToRebindKey() => pressToRebindKeyTransform.gameObject.SetActive(true);
       
        public void HidePressToRebindKey() => pressToRebindKeyTransform.gameObject.SetActive(false);

        private void RebindBinding(InputManager.Binding binding)
        {
            ShowPressToRebindKey();
            InputManager.Instance.RebindBinding(binding,() =>
            {
                HidePressToRebindKey();
                UpdateVisual();
            });
        }
    }
}