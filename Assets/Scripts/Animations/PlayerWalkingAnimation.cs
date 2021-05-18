using System;
using UnityEngine;

namespace Animations
{
    public class PlayerWalkingAnimation : Animation
    {
        private void Awake()
        {
            SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            FrameRate = .25f;
        }

        private void Update()
        {
            Timer += Time.deltaTime;

            if (Timer >= FrameRate)
            {
                Timer -= FrameRate;
                CurrentSprite = (CurrentSprite + 1) % sprites.Length;
                SpriteRenderer.sprite = sprites[CurrentSprite];
            }
        }
    }
}
