using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
public class FollowTransformAnchorForward : MonoBehaviour
{
    [SerializeField] Transform anchor;
    [SerializeField] float constantHeight = 1f;
    [SerializeField] float constantDist = 2f;

    private void Update() {
        if(anchor != null) {
            var newForward = new Vector3(anchor.transform.forward.x, 0, anchor.transform.forward.z).normalized;
            transform.position = new Vector3(anchor.transform.position.x, anchor.transform.position.y, anchor.transform.position.z) + (newForward * constantDist);
            transform.rotation = Quaternion.LookRotation(newForward, Vector3.up);
        }
    }
}
