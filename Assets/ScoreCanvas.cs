using System.Collections;
using System.Collections.Generic;
using App.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ScoreCanvas : MonoBehaviour {
    [FormerlySerializedAs("slider")]
    [FormerlySerializedAs("_slider")]
    [SerializeField] private Slider scoreSlider;
    
    [SerializeField] private Slider timeSlider;
    [SerializeField] private CanvasGroup canvasGroup;
    private void Awake() {
        canvasGroup.alpha = 0;
        GameStateManager.Instance.OnGameStateChanged.AddListener(OnGameStateChanged);
    }

    private IEnumerator GameStarted() {
        yield return new WaitUntil(() => TileSetup.HasSetupTiles);
        
        var totalAmountOfTiles = TileSetup.TotalAmountOfTiles;
        var midPoint = totalAmountOfTiles / 2;

        GameStateManager.Score = 0;
        foreach (var tileCollisionHandler in TileSetup.TileCollisionHandlerList) {
            GameStateManager.Score += tileCollisionHandler.meshRenderer.enabled ? -1 : 1;
        }
        
        scoreSlider.minValue = -midPoint;
        scoreSlider.maxValue = midPoint;
        
        scoreSlider.value = GameStateManager.Score;
        timeSlider.maxValue = GameStateManager.Instance._gameTime;
        timeSlider.value = GameStateManager.Instance._gameTime;
        canvasGroup.alpha = 0.6f;
        StartCoroutine(UpdateScore());
    }
    
    private void OnGameStateChanged(GameState gameState) {
        if (gameState == GameState.GameStarted) {
            StartCoroutine(GameStarted());
        }
    }
    
    WaitForSeconds wait = new WaitForSeconds(0.5f);
    IEnumerator UpdateScore() {
        while (true) {
            yield return wait;
            scoreSlider.value = GameStateManager.Score;
            yield return wait;
            timeSlider.value--;
            
            if (timeSlider.value <= 0) {
                GameStateManager.Instance.CurrentGameState = GameState.GameEnded;
                break;
            }
        }
    }
}
