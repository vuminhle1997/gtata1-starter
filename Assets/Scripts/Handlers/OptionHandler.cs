using System.Collections;
using System.Collections.Generic;
using StateMachines;
using UnityEngine;

namespace Handlers
{
    public class OptionHandler : StateHandler
    {
        [SerializeField] private MenuStateMachine menuStateMachine;
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
        }
    }
}
