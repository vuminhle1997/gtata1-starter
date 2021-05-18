using System.Collections.Generic;
using StateMachines;
using UnityEngine;

namespace Handlers.GameHandler
{
    public class WinningHandler: StateHandler
    {
        [SerializeField] private GameObject winningScreenGameObject;
        public override void OnEnter(Dictionary<string, object> payload = null)
        {
            Debug.Log("Enter State: " + $"{GameState.Winning}");
            winningScreenGameObject.SetActive(true);
        }

        public override void OnExit()
        {
            Debug.Log("Exit State: " + $"{GameState.Winning}");
            winningScreenGameObject.SetActive(false);
        }
    }
}