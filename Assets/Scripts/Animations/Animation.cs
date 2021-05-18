using UnityEngine;

namespace Animations
{
    public class Animation: MonoBehaviour
    {
        // source: https://www.youtube.com/watch?v=Ap8bGol7qBk
        [SerializeField] protected Sprite[] sprites;
        protected int CurrentSprite;
        protected float Timer;
        protected float FrameRate = 1.25f;

        protected SpriteRenderer SpriteRenderer;
    }
}