using System;
using UnityEngine;

namespace Utils
{
    public class VaccineBulletController : MonoBehaviour
    {
        private Vector2 _originPos;
        private Rigidbody2D rb;
        private Vector2 v0;
        private Vector2 n;
        private void Awake()
        {
            _originPos = transform.position;
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            v0 = rb.velocity;
            n = v0.normalized;
        }

        private void Update()
        {
            SlowDownVelocityEachFrame();
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

        /// <summary>
        /// If vaccine touches obstacle, destroy it!
        /// </summary>
        /// <param name="other"></param>
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

        /// <summary>
        /// Slows down the velocity of the bullet by each frame.
        /// If the starting velocity was 300s/t, s = distance unit and t = time
        /// Then in the next frame, the next velocity of the bullet is less
        /// (e.g. 250s/t, and in the next frame maybe 175s/t)
        /// In short, 300s/t means the bullet travels 300 vector unit in the specific direction
        /// in one time unit t. 
        /// </summary>
        private void SlowDownVelocityEachFrame()
        {
            rb.velocity = rb.velocity + (-120f * Time.deltaTime * n);
        }
    }
}
