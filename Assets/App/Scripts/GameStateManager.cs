using System;
using UnityEngine;
using UnityEngine.Events;

namespace App.Scripts
{
    public class GameStateManager : MonoSingleton<GameStateManager>
    {
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
        }
    }

    public enum GameState
    {
        AnchorsSetup,
        GameStarted,
    }
}