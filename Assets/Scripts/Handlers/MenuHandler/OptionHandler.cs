using System.Collections.Generic;
using Persistence;
using StateMachines;
using UnityEngine;
using UnityEngine.Serialization;

namespace Handlers.MenuHandler
{
    /// <summary>
    /// Toggles the option menu screen.
    /// </summary>
    public class OptionHandler : StateHandler
    {
        [FormerlySerializedAs("_settings")] [SerializeField] private Settings settings;
        [SerializeField] private GameObject optionMenuGameObject;
        public override void OnEnter(Dictionary<string, object> payload = null)
        {
            Debug.Log("Enter State: " + $"{MenuState.Option}");
            optionMenuGameObject.SetActive(true);
        }

        public override void OnExit()
        {
            Debug.Log("Exit State: " + $"{MenuState.Option}");
            optionMenuGameObject.SetActive(false);
            var settingsOptions = settings.settingsOptions;
            
            #if UNITY_EDITOR
                Settings.SaveSettings(settingsOptions, Settings.PATH);
            #else
                Settings.SaveSettings(settingsOptions, Settings.BIN_PATH);
            #endif
        }
    }
}
