using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameToggleSprite : MonoBehaviour
{
    [SerializeField] private GameStateMachine gameStateMachine;
    [SerializeField] private SpriteRenderer[] spriteRenderers;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        var currentState = gameStateMachine.GetCurrentState();
        switch (currentState)
        {  
            case GameState.Play:
                var playImg = spriteRenderers[1];
                image.sprite = playImg.sprite;
                break;
            case GameState.Pause:
                var pauseImg = spriteRenderers[0];
                image.sprite = pauseImg.sprite;
                break;
            default:
                throw new NotImplementedException("Not implemented yet");
        }
    }
}
