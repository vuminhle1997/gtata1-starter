using System.Collections;
using System.Collections.Generic;
using Persistence;
using StateMachines;
using UnityEngine;

namespace Handlers
{
    public class OptionHandler : StateHandler
    {
        [SerializeField] private MenuStateMachine menuStateMachine;
        [SerializeField] private Settings _settings;
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
            var settingsOptions = _settings.settingsOptions;

            Settings.SaveSettings(settingsOptions, Settings.PATH);
        }
    }
}
