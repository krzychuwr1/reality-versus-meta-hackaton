using Photon.Pun;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    [SerializeField]
    private OVRInput.Controller controller;
    [SerializeField]
    private BulletBehaviour bulletA;
    [SerializeField]
    private BulletBehaviour bulletB;
    [SerializeField]
    private Transform nozzle;
    [SerializeField][Range(1f, 100f)]
    private float force = 5f;

    public bool IsVirtual { get; set; }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller))
        {
            BulletBehaviour bullet = IsVirtual ? bulletA : bulletB;

            var position = nozzle.position;
            GameObject networkedBullet = PhotonNetwork.Instantiate(bullet.name, position, nozzle.rotation);
            Rigidbody body = networkedBullet.GetComponent<Rigidbody>();
            body.AddForce(nozzle.forward * force, ForceMode.Impulse);
            AudioSource.PlayClipAtPoint(AudioManager.Instance.gunShotAudio, position);
        }
    }
}
