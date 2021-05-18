using UnityEngine;

// source: https://www.youtube.com/watch?v=MFQhpwc6cKE

    /// <summary>
    /// Script for "Main Camera"
    /// Follows the target a.k.a the player gameobject
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;

        public float smoothSpeed = 0.125f;
        private static float zOffset = -230f;
        private Vector3 offset = new Vector3(0, 0, zOffset);

        void LateUpdate()
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
            transform.LookAt(target);
        }
    }

