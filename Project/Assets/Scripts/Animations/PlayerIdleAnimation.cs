using System;
using UnityEngine;

namespace Animations
{
    public class PlayerIdleAnimation : Animation
    {
        private void Awake()
        {
            SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// In each framerate, change the sprite renderer by using another serialized sprite
        /// </summary>
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
