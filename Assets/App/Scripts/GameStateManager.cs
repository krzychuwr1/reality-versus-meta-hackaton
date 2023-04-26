using System;
using UnityEngine;
using UnityEngine.Events;

namespace App.Scripts
{
    public class GameStateManager : MonoSingleton<GameStateManager>
    {
        private GameState _currentGameState;
        public UnityEvent<GameState> OnGameStateChanged;

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