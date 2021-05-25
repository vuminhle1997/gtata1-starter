using System;
using System.Collections.Generic;
using Handlers;
using UnityEngine;

namespace StateMachines
{
    /// <summary>
    /// The game state machine.
    /// </summary>
    public class GameStateMachine: MonoBehaviour
    {
        [SerializeField] private GameState currentState;
        [SerializeField] private StateHandler playHandler;
        [SerializeField] private StateHandler gameOverHandler;
        [SerializeField] private StateHandler menuHandler;
        [SerializeField] private StateHandler levelSuccessHandler;
        [SerializeField] private StateHandler winningHandler;

        /// <summary>
        /// Default game state. PLAY
        /// </summary>
        private void Awake()
        {
            currentState = GameState.Play;
        }

        /// <summary>
        /// Checks the game's state. Toggle's in-game menu.
        /// </summary>
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
        /// Makes transition to another state.
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
                case GameTransition.ShowGameOver:
                    if (currentState == GameState.Play)
                    {
                        TransitionTo(GameState.GameOver, payload);
                        return true;
                    }

                    return false;
                case GameTransition.ShowLevelSuccess:
                    if (currentState == GameState.Play)
                    {
                        TransitionTo(GameState.LevelSuccess, payload);
                        return true;
                    }

                    return false;
                case GameTransition.ShowWinningScreen:
                    if (currentState == GameState.Play)
                    {
                        TransitionTo(GameState.Winning, payload);
                        return true;
                    }

                    return false;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Internal method for Trigger
        /// </summary>
        /// <param name="newState"></param>
        /// <param name="payload"></param>
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

        #region Getters

        /// <summary>
        /// Returns a specific state handler by the given parameter.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
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

        /// <summary>
        /// Returns the current game state.
        /// </summary>
        /// <returns></returns>
        public GameState GetCurrentGameState()
        {
            return currentState;
        }

        #endregion
    }
}