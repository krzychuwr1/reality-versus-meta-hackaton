using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        var tileCollisionHandler = collision.gameObject.GetComponent<TileToggleHelper>();
        if(tileCollisionHandler != null) {
            tileCollisionHandler.Toggle();
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Grid"))
        {
            Destroy(gameObject);
        }
    }
}
