using System.Collections.Generic;
using Persistence;
using StateMachines;
using UI.Menu;
using UnityEngine;

namespace Handlers.MenuHandler
{
    /// <summary>
    /// Toggles the main menu screen.
    /// </summary>
    public class MenuHandler : StateHandler
    {
        [SerializeField] private MenuStateMachine menuStateMachine;
        [SerializeField] private GameObject menuMainGameObject;
        [SerializeField] private MenuButtonsController menuButtonsController;
        public override void OnEnter(Dictionary<string, object> payload = null)
        {
            SettingsOptions settingsOptions;
#if UNITY_EDITOR
            Debug.Log("Enter State: " + $"{MenuState.Menu}");
            settingsOptions = Settings.LoadSettings(Settings.PATH);
#else
            settingsOptions = Settings.LoadSettings(Settings.BIN_PATH);
#endif

            if (settingsOptions._enableMusic)
            {
                menuStateMachine.GetMusicSource().Play();
                
            }
            else
            {
                menuStateMachine.GetMusicSource().Pause();
            }
            menuStateMachine.GetMusicSource().volume = settingsOptions._musicLevel / 100;

            if (settingsOptions._enableSound)
            {
                menuButtonsController.GetAudioSource().mute = false;
            }
            else
            {
                menuButtonsController.GetAudioSource().mute = true;
            }
            menuButtonsController.GetAudioSource().volume = settingsOptions._soundLevel / 100;
            
            menuMainGameObject.SetActive(true);
        }

        public override void OnExit()
        {
#if UNITY_EDITOR
            Debug.Log("Exit State: " + $"{MenuState.Menu}");
#endif
            menuMainGameObject.SetActive(false);
        }
    }
}
