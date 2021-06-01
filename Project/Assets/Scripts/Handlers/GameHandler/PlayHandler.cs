using System.Collections.Generic;
using StateMachines;
using UnityEngine;

namespace Handlers.GameHandler
{
    /// <summary>
    /// Toggles the player's UI.
    /// </summary>
    public class PlayHandler: StateHandler
    {
        [SerializeField] private GameObject mainScreenOverlay;
        public override void OnEnter(Dictionary<string, object> payload = null)
        {
#if UNITY_EDITOR
            Debug.Log("Enter State: " + $"{GameState.Play}");
#endif
            mainScreenOverlay.SetActive(true);
        }

        public override void OnExit()
        {
#if UNITY_EDITOR
            Debug.Log("Exit State: " + $"{GameState.Play}");
#endif
            mainScreenOverlay.SetActive(false);
        }
    }
}