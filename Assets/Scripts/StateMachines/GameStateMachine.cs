using System;
using System.Collections.Generic;
using Handlers;
using UnityEngine;

namespace StateMachines
{
    public class GameStateMachine: MonoBehaviour
    {
        [SerializeField] private GameState currentState;
        [SerializeField] private StateHandler playHandler;
        [SerializeField] private StateHandler gameOverHandler;
        [SerializeField] private StateHandler pauseHandler;
        [SerializeField] private StateHandler menuHandler;

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
                    if (currentState == GameState.Play || currentState == GameState.Pause)
                    {
                        TransitionTo(GameState.Play, payload);
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
                case GameState.GameOver:
                    return gameOverHandler;
                case GameState.Pause:
                    return pauseHandler;
                case GameState.Menu:
                    return menuHandler;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}