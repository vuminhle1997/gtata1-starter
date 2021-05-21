using UnityEngine;

namespace Actor
{
    public abstract class PlayableActor: MonoBehaviour, IUnit, IActor
    {
        public abstract void Jump();
        public abstract void FireBullet();

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
    }
}