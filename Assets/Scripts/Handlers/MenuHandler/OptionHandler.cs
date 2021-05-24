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

            Settings.SaveSettings(settingsOptions, Settings.Path);
        }
    }
}
