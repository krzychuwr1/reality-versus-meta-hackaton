using System;
using App.Scripts;
using Photon.Pun;
using UnityEngine;

namespace App.Prefabs.PhotonPrefabSpawner
{
    public class OnGameStartPhotonPrefabSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject _prefabToSpawn;
    
        void Start()
        {
            GameStateManager.Instance.OnGameStateChanged.AddListener(InstantiatePrefab);
        }

        private void InstantiatePrefab(GameState state)
        {
            if (state == GameState.GameStarted)
            {
                PhotonNetwork.Instantiate(_prefabToSpawn.name, transform.position, Quaternion.identity);
            }
        }

        private void OnDestroy()
        {
            if (GameStateManager.Instance.OnGameStateChanged != null)
                GameStateManager.Instance.OnGameStateChanged.RemoveListener(InstantiatePrefab);
        }
    }
}
