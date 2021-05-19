using System.Collections;
using System.Collections.Generic;
using Persistence;
using StateMachines;
using UnityEngine;
using UnityEngine.Serialization;

namespace Handlers
{
    public class OptionHandler : StateHandler
    {
        [SerializeField] private MenuStateMachine menuStateMachine;
        [FormerlySerializedAs("_settings")] [SerializeField] private Settings settings;
        public override void OnEnter(Dictionary<string, object> payload = null)
        {
            Debug.Log("Enter State: " + $"{MenuState.Option}");
            var optionMenu = menuStateMachine.GetGameObject(MenuState.Option);
            menuStateMachine.EnableGameObject(optionMenu);
        }

        public override void OnExit()
        {
            Debug.Log("Exit State: " + $"{MenuState.Option}");
            var optionMenu = menuStateMachine.GetGameObject(MenuState.Option);
            menuStateMachine.DisableGameObject(optionMenu);
            var settingsOptions = settings.settingsOptions;

            Settings.SaveSettings(settingsOptions, Settings.Path);
        }
    }
}
