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
        // [SerializeField] private MenuStateMachine menuStateMachine;
        [FormerlySerializedAs("_settings")] [SerializeField] private Settings settings;
        [SerializeField] private GameObject optionMenuGameObject;
        public override void OnEnter(Dictionary<string, object> payload = null)
        {
            Debug.Log("Enter State: " + $"{MenuState.Option}");
            // var optionMenu = menuStateMachine.GetGameObject(MenuState.Option);
            // menuStateMachine.EnableGameObject(optionMenu);
            optionMenuGameObject.SetActive(true);
        }

        public override void OnExit()
        {
            Debug.Log("Exit State: " + $"{MenuState.Option}");
            // var optionMenu = menuStateMachine.GetGameObject(MenuState.Option);
            // menuStateMachine.DisableGameObject(optionMenu);
            optionMenuGameObject.SetActive(false);
            var settingsOptions = settings.settingsOptions;

            Settings.SaveSettings(settingsOptions, Settings.Path);
        }
    }
}
