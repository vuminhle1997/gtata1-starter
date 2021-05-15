using System;
using System.Collections;
using System.Collections.Generic;
using StateMachines;
using UnityEngine;

namespace Handlers
{
    public class MenuHandler : StateHandler
    {
        [SerializeField] private MenuStateMachine menuStateMachine;
        public override void OnEnter(Dictionary<string, object> payload = null)
        {
            Debug.Log("Enter State: " + $"{MenuState.Menu}");
            var mainMenu = menuStateMachine.GetGameObject(MenuState.Menu);
            menuStateMachine.EnableGameObject(mainMenu);
        }

        public override void OnExit()
        {
            Debug.Log("Exit State: " + $"{MenuState.Menu}");
            var mainMenu = menuStateMachine.GetGameObject(MenuState.Menu);
            menuStateMachine.DisableGameObject(mainMenu);
        }
    }
}
