using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaySizeHelper : MonoBehaviour
{
    [SerializeField] private float height;
    [SerializeField] private Vector3 size;

    // Update is called once per frame
    void Start ()
    {
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
        transform.localScale = gameObject.transform.InverseTransformVector(size);

    }
}
