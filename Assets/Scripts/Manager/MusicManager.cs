using System;
using UnityEngine;

namespace Manager
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager Instance { get; private set; }
        
        [SerializeField] private AudioSource audioSource;
        private float volume = 0.3f;

        private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";

        private void Awake()
        {
            if(Instance != null) Debug.LogError("Music Manager is already initialized");
            Instance = this;
            
            LoadAudioSourceComponent();

            volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, 0.3f);
            audioSource.volume = volume;
        }

        private void LoadAudioSourceComponent()
        {
            if (this.audioSource != null) return;
            this.audioSource = GetComponent<AudioSource>();
        }

        public void ChangeVolume()
        {
            volume += 0.1f;
            if (volume > 1) volume = 0;
            audioSource.volume = this.volume;
            
            PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
            PlayerPrefs.Save();
        }
        
        public float GetVolume() => this.volume;
    }
}