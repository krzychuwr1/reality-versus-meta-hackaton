using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioClip tileHitAudio;
    public AudioClip gunShotAudio;
    public AudioClip gameMusic;
    public AudioClip menuMusic;
    private static AudioManager _instance;
    private AudioSource _audioSource;
    public static AudioManager Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<AudioManager>();
            }
            return _instance;
        }
    }
    
    private void Awake() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
        
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.loop = true;
        _audioSource.clip = menuMusic;
        _audioSource.volume = 0;
        _audioSource.Play();
        FadeAudio(1, 4);
        GameStateManager.Instance.OnGameStateChanged.AddListener(OnGameStateChanged);
    }
    
    private void OnGameStateChanged(GameState gameState) {
        if (gameState == GameState.GameStarted) {
            FadeAudio(0, 2,() => {
                _audioSource.clip = gameMusic;
                _audioSource.Play();
                FadeAudio(1, 2);
            });
        } else if (gameState == GameState.GameEnded) {
            FadeAudio(0, 2,() => {
                _audioSource.clip = menuMusic;
                _audioSource.Play();
                FadeAudio(1, 2);
            });
        }
    }

    private void FadeAudio(float volume, float time, Action onComplete = null) {
        StartCoroutine(FadeAudioCoroutine(volume, onComplete));
        IEnumerator FadeAudioCoroutine(float targetVolume, Action onComplete) {
            var startVolume = _audioSource.volume;
            var startTime = Time.time;
            while (Time.time - startTime < time) {
                _audioSource.volume = Mathf.Lerp(startVolume, targetVolume, (Time.time - startTime) / time);
                yield return null;
            }
            _audioSource.volume = targetVolume;
            onComplete?.Invoke();
        }
    }
}
