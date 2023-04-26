using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepSizeHelper : MonoBehaviour
{
    [SerializeField] private float height;
    [SerializeField] private Vector3 size;

    // Update is called once per frame
    void Start ()
    {
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
        var localscale = gameObject.transform.InverseTransformVector(size);
        transform.localScale = new Vector3(Mathf.Abs(localscale.x), Mathf.Abs(localscale.y), Mathf.Abs(localscale.z) );

    }
}
