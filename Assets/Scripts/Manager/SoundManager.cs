using System;
using KitchenObjects.Counter;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Manager
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }
        
        [SerializeField] private AudioClipRefsSO audioClipRefsSO;
        private float volume = 1f;

        private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

        private void Awake()
        {
            if (Instance != null) Debug.LogError("SoundManger is already initialized");
            Instance = this;

            volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
        }

        private void Start()
        {
            DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
            DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
            CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
            PlayerController.Instance.GetPlayerInteraction().OnPickedSomething += PlayerInteraction_OnPickedSomething;
            BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
            TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
        }

        private void DeliveryManager_OnRecipeCompleted(object sender, EventArgs e)
        {
            DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
            PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
        }

        private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
        {
            DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
            PlaySound(audioClipRefsSO.deliveryFail, deliveryCounter.transform.position);
        }

        private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
        {
            CuttingCounter cuttingCounter = sender as CuttingCounter;
            PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
        }

        private void PlayerInteraction_OnPickedSomething(object sender, EventArgs e)
        {
            PlaySound(audioClipRefsSO.objectPickup, PlayerController.Instance.transform.position);
        }

        private void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs e)
        {
            BaseCounter baseCounter = sender as BaseCounter;
            PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
        }

        private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e)
        {
            TrashCounter trashCounter = sender as TrashCounter;
            PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
        }

        private void PlaySound(AudioClip[] audioClipArray, Vector3 point, float volume = 1f)
        {
            PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], point, volume);
        }
        
        private void PlaySound(AudioClip audioClip, Vector3 point, float volumeMultiplier = 1f)
        {
            AudioSource.PlayClipAtPoint(audioClip, point, volumeMultiplier * volume);
        }

        public void PlayFootstepSound(Vector3 position, float volume)
        {
            PlaySound(audioClipRefsSO.footstep, position, volume);
        }

        public void ChangeVolume()
        {
            volume += 0.1f;
            if (volume > 1) volume = 0;
            
            PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
            PlayerPrefs.Save();
        }
        
        public float GetVolume() => this.volume;
    }
}