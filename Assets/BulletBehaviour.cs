using System;
using App.Scripts.Utilities;
using UnityEngine;
using Photon.Pun;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField]
    private bool isVirtual;

    private GameObject _trail;

    private GameObject _trailInstance;

    private void Start()
    {
        this.ExecuteDelayed(() =>
        {
            if (gameObject) PhotonNetwork.Destroy(gameObject);
        }, 5f);
        if (!_trail) return;
        var bulletTransform = transform;
        _trailInstance = Instantiate(_trail, bulletTransform.position, bulletTransform.rotation);
    }

    private void Update()
    {
        if (!_trailInstance) return;
        var bulletTransform = transform;
        _trailInstance.transform.position = bulletTransform.position;
        _trailInstance.transform.rotation = bulletTransform.rotation;
    }

    private void OnDestroy()
    {
        if (!_trailInstance) return;
        Destroy(_trailInstance.gameObject, 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        var tileCollisionHandler = other.gameObject.GetComponent<TileToggleHelper>();
        if (tileCollisionHandler == null) return;
        tileCollisionHandler.Toggle(isVirtual);
        PhotonNetwork.Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var tileCollisionHandler = collision.gameObject.GetComponent<TileToggleHelper>();
        if (tileCollisionHandler != null) {
            tileCollisionHandler.Toggle(isVirtual);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
