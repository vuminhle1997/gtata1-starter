using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine: MonoBehaviour
{
    [SerializeField] private GameState currentState;
    [SerializeField] private StateHandler playHandler;
    [SerializeField] private StateHandler pauseHandler;
    [SerializeField] private StateHandler menuHandler;
    [SerializeField] private StateHandler winningHandler;
    [SerializeField] private StateHandler gameOverHandler;

    /// <summary>
    /// Go back to main menu!
    /// </summary>
    public void Reset()
    {
        currentState = GameState.Menu;
    }

    public void RegisterHandler(GameState gameState, StateHandler stateHandler)
    {
        switch (gameState)
        {
            case GameState.Play:
                stateHandler.OnEnter();
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="triggerType"></param>
    /// <param name="payload"></param>
    /// <returns></returns>
    public bool Trigger(GameTransition triggerType, Dictionary<string, object> payload = null)
    {
        switch (triggerType)
        {
            case GameTransition.ResumePlaying:
                if (currentState == GameState.Pause)
                {
                    TransitionTo(GameState.Play, payload);
                    return true;
                }
                
                return false;
            case GameTransition.StopPlaying:
                if (currentState == GameState.Play)
                {
                    TransitionTo(GameState.Pause, payload);
                    return true;
                }

                return false;
        }
        return false;
    }

    private void TransitionTo(GameState newState, Dictionary<string, object> payload)
    {
        if (newState == currentState)
        {
            return;
        }

        var currentHandler = GetHandler(currentState);
        var nextHandler = GetHandler(newState);
        
        currentHandler.OnExit();
        nextHandler.OnEnter(payload);

        currentState = newState;
    }

    private StateHandler GetHandler(GameState state)
    {
        switch (state)
        {
            case GameState.Play:
                return playHandler;
            case GameState.Pause:
                return pauseHandler;
            case GameState.Menu:
                return menuHandler;
            case GameState.Winning:
                return winningHandler;
            case GameState.GameOver:
                return gameOverHandler;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    public GameState GetCurrentState()
    {
        return currentState;
    }
}