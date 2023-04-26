using System;
using UnityEngine;

namespace App.Scripts
{
    public class GameStateManager : MonoSingleton<GameStateManager>
    {
        public GameState CurrentGameState;

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