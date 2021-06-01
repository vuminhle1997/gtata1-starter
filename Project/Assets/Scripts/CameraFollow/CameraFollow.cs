using UnityEngine;

// source: https://www.youtube.com/watch?v=MFQhpwc6cKE

namespace CameraFollow
{
    /// <summary>
    /// Script for "Main Camera"
    /// Follows the target a.k.a the player gameobject
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        private static float zOffset = -230f;
        private readonly Vector3 _offset = new Vector3(0, 0, zOffset);

        void LateUpdate()
        {
            Vector3 desiredPosition = target.position + _offset;
            transform.position = desiredPosition;
            transform.LookAt(target);
        }
    }
}

