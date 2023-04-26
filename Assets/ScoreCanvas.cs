using System.Collections;
using System.Collections.Generic;
using App.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ScoreCanvas : MonoBehaviour {
    [FormerlySerializedAs("_slider")]
    [SerializeField] private Slider slider;
    [SerializeField] private CanvasGroup canvasGroup;
    private void Awake() {
        canvasGroup.alpha = 0;
    }

    private IEnumerator Start() {
        yield return new WaitUntil(() => TileSetup.HasSetupTiles);
        
        var totalAmountOfTiles = TileSetup.TotalAmountOfTiles;
        var midPoint = totalAmountOfTiles / 2;

        GameStateManager.Score = 0;
        foreach (var tileCollisionHandler in TileSetup.TileCollisionHandlerList) {
            GameStateManager.Score += tileCollisionHandler.meshRenderer.enabled ? -1 : 1;
        }
        
        slider.minValue = -midPoint;
        slider.maxValue = midPoint;
        
        slider.value = GameStateManager.Score;
        canvasGroup.alpha = 1;
        
        GameStateManager.Instance.OnGameStateChanged.AddListener(OnGameStateChanged);
        GameStateManager.Instance.CurrentGameState = GameState.GameStarted;
        OnGameStateChanged(GameStateManager.Instance.CurrentGameState);
    }
    
    private void OnGameStateChanged(GameState gameState) {
        if (gameState == GameState.GameStarted) {
            StartCoroutine(UpdateScore());
        }
    }
    
    WaitForSeconds wait = new WaitForSeconds(0.5f);
    IEnumerator UpdateScore() {
        while (true) {
            yield return wait;
            slider.value = GameStateManager.Score;
        }
    }
}
