using UnityEngine;

namespace Animations
{
    public class Animation: MonoBehaviour
    {
        // source: https://www.youtube.com/watch?v=Ap8bGol7qBk
        [SerializeField] protected Sprite[] sprites;
        protected int _currentSprite;
        protected float _timer;
        protected float _frameRate = 1.25f;

        protected SpriteRenderer _spriteRenderer;
    }
}