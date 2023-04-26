using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var tileCollisionHandler = other.gameObject.GetComponent<TileToggleHelper>();
        if(tileCollisionHandler != null) {
            tileCollisionHandler.Toggle();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var tileCollisionHandler = collision.gameObject.GetComponent<TileToggleHelper>();
        if(tileCollisionHandler != null) {
            tileCollisionHandler.Toggle();
            Destroy(gameObject);
        }
    }
}
