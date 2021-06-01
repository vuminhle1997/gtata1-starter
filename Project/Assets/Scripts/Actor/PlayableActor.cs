using UnityEngine;

namespace Actor
{
    /// <summary>
    /// Abstract class/skeleton for an playable actor 
    /// </summary>
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