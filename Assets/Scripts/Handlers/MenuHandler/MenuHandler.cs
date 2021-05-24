using System;
using System.Collections;
using System.Collections.Generic;
using StateMachines;
using UnityEngine;

namespace Handlers
{
    public class MenuHandler : StateHandler
    {
        // [SerializeField] private MenuStateMachine menuStateMachine;
        [SerializeField] private GameObject menuMainGameObject;
        public override void OnEnter(Dictionary<string, object> payload = null)
        {
            Debug.Log("Enter State: " + $"{MenuState.Menu}");
            menuMainGameObject.SetActive(true);
        }

        public override void OnExit()
        {
            Debug.Log("Exit State: " + $"{MenuState.Menu}");
            menuMainGameObject.SetActive(false);
        }
    }
}
