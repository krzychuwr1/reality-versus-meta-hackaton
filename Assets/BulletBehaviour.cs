using App.Scripts.Utilities;
using UnityEngine;
using Photon.Pun;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField]
    private bool isVirtual;

    private void Start()
    {
        this.ExecuteDelayed(() =>
        {
            if (gameObject)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }, 5.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        var tileCollisionHandler = other.gameObject.GetComponent<TileToggleHelper>();
        if(tileCollisionHandler != null) {
            tileCollisionHandler.Toggle(isVirtual);
            PhotonNetwork.Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var tileCollisionHandler = collision.gameObject.GetComponent<TileToggleHelper>();
        if(tileCollisionHandler != null) {
            tileCollisionHandler.Toggle(isVirtual);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
