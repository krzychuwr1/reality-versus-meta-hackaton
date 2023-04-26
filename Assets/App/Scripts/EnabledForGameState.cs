using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts
{
    public class EnabledForGameState : MonoBehaviour
    {
        [SerializeField] private List<GameState> _gameStates;

        [SerializeField] private List<GameObject> _gameObjects;

        void Update()
        {
            if (_gameObjects == null) return;
            foreach (var o in _gameObjects)
            {
                o.SetActive(_gameStates != null &&
                            _gameStates.Contains(GameStateManager.Instance.CurrentGameState));
            }
        }
    }
}