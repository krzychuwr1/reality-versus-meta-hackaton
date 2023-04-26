using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace App.Scripts
{
    public class GameStateManager : MonoSingleton<GameStateManager>
    {
        public float _gameTime = 60f;
        public float starttime;

        private GameState _currentGameState;
        public UnityEvent<GameState> OnGameStateChanged = new UnityEvent<GameState>();
        
        // Positive score is for the virtual team, negative score is for the reality team
        public static int Score { get; set; }

        public GameState CurrentGameState
        {
            get => _currentGameState;
            set
            {
                var previousGameState = _currentGameState;
                _currentGameState = value;
                if (previousGameState != _currentGameState)
                {
                    OnGameStateChanged.Invoke(_currentGameState);
                }
            }
        }

        private void Awake()
        {
            this.CurrentGameState = GameState.AnchorsSetup;
        }

        public void StartGame()
        {
            this.CurrentGameState = GameState.GameStarted;
            starttime = Time.time;
            StopAllCoroutines();
            StartCoroutine(PopUpController());
        }

        private IEnumerator PopUpController()
        {
            if (GameView.Instance == null)
            {
                Debug.LogError("Something os wrong with GameView, it's null");
                yield break;
            }
            GameView.Instance.ShowGamePopupPanel("Have Fun!", "3 Minutes left.", 3);
            yield return new WaitForSeconds(60);
            GameView.Instance.ShowGamePopupPanel("2 Minutes", "left", 3);
            yield return new WaitForSeconds(60);
            GameView.Instance.ShowGamePopupPanel("1 Minute", "left", 3);
            yield return new WaitForSeconds(57);
            GameView.Instance.ShowGamePopupPanel("3", "", 0.8f);
            yield return new WaitForSeconds(1);
            GameView.Instance.ShowGamePopupPanel("2", "", 0.8f);
            yield return new WaitForSeconds(1);
            GameView.Instance.ShowGamePopupPanel("1", "", 0.8f);
            yield return new WaitForSeconds(1);
            if(Score > 0) {
                GameView.Instance.ShowGamePopupPanel("Virtual Team wins", "", 5f);
            } else {
                GameView.Instance.ShowGamePopupPanel("Reality Team wins", "", 5f);
            }
        }
    }

    public enum GameState
    {
        AnchorsSetup,
        GameStarted,
        GameEnded,
    }
}