using System.Collections.Generic;
using StateMachines;
using UnityEngine;

namespace Handlers.GameHandler
{
    public class PlayHandler: StateHandler
    {
        [SerializeField] private GameObject mainScreenOverlay;
        public override void OnEnter(Dictionary<string, object> payload = null)
        {
            Debug.Log("Enter State: " + $"{GameState.Play}");
            mainScreenOverlay.SetActive(true);
        }

        public override void OnExit()
        {
            Debug.Log("Exit State: " + $"{GameState.Play}");
            mainScreenOverlay.SetActive(false);
        }
    }
}