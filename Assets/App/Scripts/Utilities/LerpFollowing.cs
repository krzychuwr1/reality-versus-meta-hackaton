using UnityEngine;

namespace App.Scripts.Utilities
{
    public class LerpFollowing : MonoBehaviour
    {
        public Transform targetTransform;
        public float speed = 1.0f;
        public bool useX = true;
        public bool useY = true;
        public bool useZ = true;

        private Vector3 targetPosition;

        void Start()
        {
            targetPosition = transform.position;
        }

        void Update()
        {
            if (targetTransform != null)
            {
                Vector3 targetPos = targetTransform.position;
                if (!useX) targetPos.x = transform.position.x;
                if (!useY) targetPos.y = transform.position.y;
                if (!useZ) targetPos.z = transform.position.z;

                targetPosition = Vector3.Lerp(targetPosition, targetPos, speed * Time.deltaTime);
                transform.position = targetPosition;
            }
        }
    }
}