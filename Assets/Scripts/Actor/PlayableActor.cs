using UnityEngine;

namespace Actor
{
    public abstract class PlayableActor: MonoBehaviour, IUnit, IActor
    {
        public abstract void Jump();
        public abstract void FireBullet();

        public abstract void Run();

        public float Health
        {
            get;
            set;
        }

        public bool Alive
        {
            get;
            set;
        }

        public int Bullets
        {
            get;
            set;
        }

        protected void TakeDamage(float damage)
        {
            Health += damage;
        }
    }
}