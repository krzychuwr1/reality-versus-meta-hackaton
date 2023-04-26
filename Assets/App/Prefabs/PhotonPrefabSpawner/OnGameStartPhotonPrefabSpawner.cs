using System;
using App.Scripts;
using Photon.Pun;
using UnityEngine;

namespace App.Prefabs.PhotonPrefabSpawner
{
    public class OnGameStartPhotonPrefabSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject weaponA;
        [SerializeField]
        private GameObject weaponB;

        void Start()
        {
            GameStateManager.Instance.OnGameStateChanged.AddListener(InstantiatePrefab);
        }

        private void InstantiatePrefab(GameState state)
        {
            if (state == GameState.GameStarted)
            {
                bool isVirtual = PhotonNetwork.LocalPlayer.ActorNumber % 2 > 0;

                GameObject weapon = Instantiate(isVirtual ? weaponA : weaponB, transform);
                weapon.GetComponent<WeaponBehaviour>().IsVirtual = isVirtual;
            }
        }

        private void OnDestroy()
        {
            if (GameStateManager.Instance.OnGameStateChanged != null)
                GameStateManager.Instance.OnGameStateChanged.RemoveListener(InstantiatePrefab);
        }
    }
}
