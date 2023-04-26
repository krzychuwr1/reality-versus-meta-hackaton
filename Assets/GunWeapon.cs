using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GunWeapon : MonoBehaviour
{
    [SerializeField]
    private OVRInput.Controller controller;
    [SerializeField]
    private BulletBehaviour bullet;
    [SerializeField]
    private Transform nozzle;
    [SerializeField][Range(1f, 100f)]
    private float force = 5f;

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller) || Input.GetKeyDown(KeyCode.Space))
        {
            BulletBehaviour instantiatedBullet = Instantiate(bullet, nozzle.position, nozzle.rotation);
            Rigidbody body = instantiatedBullet.GetComponent<Rigidbody>();
            body.AddForce(nozzle.forward * force, ForceMode.Impulse);
        }
    }
}
