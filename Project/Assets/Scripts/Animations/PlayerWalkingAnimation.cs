using System;
using UnityEngine;

namespace Animations
{
    public class PlayerWalkingAnimation : Animation
    {
        private void Awake()
        {
            SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            FrameRate = .25f; // idle animation has a longer framerate to switch sprite
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
