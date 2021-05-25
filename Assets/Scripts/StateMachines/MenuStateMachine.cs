using System;
using System.Collections.Generic;
using Handlers;
using UnityEngine;

namespace StateMachines
{
    /// <summary>
    /// The main menu state machine.
    /// </summary>
    public class MenuStateMachine : MonoBehaviour
    {
        [SerializeField] private MenuState currentState;
        [SerializeField] private StateHandler menuHandler, optionHandler, highScoreHandler;

        private void Awake()
        {
            currentState = MenuState.Menu;
        }

        public bool Trigger(MenuStateTransition triggerType, Dictionary<string, object> payload = null)
        {
            switch (triggerType)
            {
                case MenuStateTransition.ShowOptions:
                    if (currentState == MenuState.Menu)
                    {
                        TransitionTo(MenuState.Option, payload);
                        return true;
                    }

                    return false;
                case MenuStateTransition.ShowMenu:
                    if (currentState == MenuState.Option || currentState == MenuState.HighScore)
                    {
                        TransitionTo(MenuState.Menu, payload);
                        return true;
                    }

                    return false;
                case MenuStateTransition.ShowHighscore:
                    if (currentState == MenuState.Menu)
                    {
                        TransitionTo(MenuState.HighScore, payload);
                        return true;
                    }

                    return false;
                default:
                    throw new ArgumentOutOfRangeException(nameof(triggerType), triggerType, null);
            }
        }

        private void TransitionTo(MenuState newState, Dictionary<string, object> payload)
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

        private StateHandler GetHandler(MenuState type)
        {
            switch (type)
            {
                case MenuState.Menu:
                    return menuHandler;
                case MenuState.Option:
                    return optionHandler;
                case MenuState.HighScore:
                    return highScoreHandler;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
