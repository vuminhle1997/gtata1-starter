using System;
using System.Collections.Generic;
using Handlers;
using Persistence;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace StateMachines
{
    /// <summary>
    /// The main menu state machine.
    /// </summary>
    public class MenuStateMachine : MonoBehaviour
    {
        [SerializeField] private MenuState currentState;
        [SerializeField] private StateHandler menuHandler, optionHandler, highScoreHandler;
        private AudioSource musicSource;

        private void Awake()
        {
            currentState = MenuState.Menu;
            musicSource = GetComponent<AudioSource>();
            SettingsOptions settingsOptions;
            
            #if UNITY_EDITOR
                settingsOptions = Settings.LoadSettings(Settings.PATH);
            #else
                settingsOptions = Settings.LoadSettings(Settings.BIN_PATH);
            #endif

            if (settingsOptions._enableMusic)
            {
                musicSource.Play();
                musicSource.volume = settingsOptions._musicLevel / 100;
            }
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
                case MenuStateTransition.ShowHighScore:
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

        #region Getters
        
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

        public AudioSource GetMusicSource()
        {
            return musicSource;
        }

        #endregion
    }
}
