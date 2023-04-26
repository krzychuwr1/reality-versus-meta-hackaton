using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.Utilities;
using Photon.Pun;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private void Start()
    {
        this.ExecuteDelayed(() =>
        {
            if (gameObject) PhotonNetwork.Destroy(gameObject);
        }, 5.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        var tileCollisionHandler = other.gameObject.GetComponent<TileToggleHelper>();
        if(tileCollisionHandler != null) {
            tileCollisionHandler.Toggle();
            PhotonNetwork.Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var tileCollisionHandler = collision.gameObject.GetComponent<TileToggleHelper>();
        if(tileCollisionHandler != null) {
            tileCollisionHandler.Toggle();
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
