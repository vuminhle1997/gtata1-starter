using System.Collections.Generic;
using StateMachines;
using UnityEngine;

namespace Handlers.GameHandler
{
    public class InGameMenuHandler: StateHandler
    {
        [SerializeField] private GameObject inGameMenuGameObject;
        public override void OnEnter(Dictionary<string, object> payload = null)
        {
            Debug.Log("Enter State: " + $"{GameState.Menu}");
            inGameMenuGameObject.SetActive(true);
        }

        public override void OnExit()
        {
            Debug.Log("Exit State: " + $"{GameState.Menu}");
            inGameMenuGameObject.SetActive(false);
        }
    }
}