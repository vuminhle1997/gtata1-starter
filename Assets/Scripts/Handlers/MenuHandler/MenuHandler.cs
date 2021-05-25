using System.Collections.Generic;
using StateMachines;
using UnityEngine;

namespace Handlers.MenuHandler
{
    /// <summary>
    /// Toggles the main menu screen.
    /// </summary>
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
