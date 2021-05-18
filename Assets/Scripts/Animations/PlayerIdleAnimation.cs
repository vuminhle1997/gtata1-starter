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

        private  void Update()
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
