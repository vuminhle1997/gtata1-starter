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
        [SerializeField] private StateHandler menuHandler;
        [SerializeField] private StateHandler levelSuccessHandler;
        [SerializeField] private StateHandler winningHandler;

        /// <summary>
        /// Go back to main menu!
        /// </summary>
        public void Reset()
        {
            currentState = GameState.Menu;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && currentState == GameState.Play)
            {
                Trigger(GameTransition.PausePlaying);
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && currentState == GameState.Menu)
            {
                Trigger(GameTransition.ResumePlaying);
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
                    if (currentState == GameState.Play || currentState == GameState.Menu)
                    {
                        TransitionTo(GameState.Play, payload);
                        return true;
                    }
                
                    return false;
                case GameTransition.PausePlaying:
                    if (currentState == GameState.Play)
                    {
                        TransitionTo(GameState.Menu, payload);
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
                case GameState.Menu:
                    return menuHandler;
                case GameState.GameOver:
                    return gameOverHandler;
                case GameState.LevelSuccess:
                    return levelSuccessHandler;
                case GameState.Winning:
                    return winningHandler;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        public GameState GetCurrentGameState()
        {
            return currentState;
        }
    }
}