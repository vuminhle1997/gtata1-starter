using System.Collections.Generic;
using StateMachines;
using UnityEngine;

namespace Handlers.GameHandler
{
    /// <summary>
    /// Toggles the in-game menu screen.
    /// </summary>
    public class InGameMenuHandler: StateHandler
    {
        [SerializeField] private GameObject inGameMenuGameObject;
        public override void OnEnter(Dictionary<string, object> payload = null)
        {
#if UNITY_EDITOR
            Debug.Log("Enter State: " + $"{GameState.Menu}");
#endif
            inGameMenuGameObject.SetActive(true);
        }

        public override void OnExit()
        {
#if UNITY_EDITOR
            Debug.Log("Exit State: " + $"{GameState.Menu}");
#endif
            inGameMenuGameObject.SetActive(false);
        }
    }
}