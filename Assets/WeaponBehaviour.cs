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

    private bool isMine;

    private void Start()
    {
        PhotonView view = GetComponent<PhotonView>();
        isMine = view.IsMine;
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller) && isMine)
        {
            BulletBehaviour bulletTemplate = IsVirtual ? bulletA : bulletB;

            GameObject networkedBullet = PhotonNetwork.Instantiate(bulletTemplate.name, nozzle.position, nozzle.rotation);
            Rigidbody body = networkedBullet.GetComponent<Rigidbody>();
            body.AddForce(nozzle.forward * force, ForceMode.Impulse);
        }
    }
}
