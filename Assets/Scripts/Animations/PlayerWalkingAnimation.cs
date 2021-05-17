using System;
using UnityEngine;

namespace Animations
{
    public class PlayerWalkingAnimation : Animation
    {
        private void Awake()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            _frameRate = .25f;
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _frameRate)
            {
                _timer -= _frameRate;
                _currentSprite = (_currentSprite + 1) % sprites.Length;
                _spriteRenderer.sprite = sprites[_currentSprite];
            }
        }
    }
}
