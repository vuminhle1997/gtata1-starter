using System;
using UnityEngine;

namespace Utils
{
    public class DeleteVaccineController : MonoBehaviour
    {
        private Vector2 _originPos;
        private void Awake()
        {
            _originPos = transform.position;
        }

        private void FixedUpdate()
        {
            TrackDistanceAndDeleteObject();
        }

        /// <summary>
        /// Tracks the current position of the spawned vaccine!
        /// Calculates the distance between the spawn point and the current location.
        /// If the distance reaches a certain threshold, delete this object.
        /// </summary>
        private void TrackDistanceAndDeleteObject()
        {
            Vector2 newPos = transform.position;
            var x = Math.Pow((_originPos.x - newPos.x), 2);
            var y = Math.Pow((_originPos.y - newPos.y), 2);

            var d = Math.Sqrt(x + y);
            if (d >= 300f)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            foreach (var go in other.contacts)
            {
                if (go.collider.name.ToLower().Contains("layer"))
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
