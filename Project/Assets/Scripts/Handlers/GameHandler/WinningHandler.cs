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
#if UNITY_EDITOR
            Debug.Log("Enter State: " + $"{GameState.Winning}");
#endif
            winningScreenGameObject.SetActive(true);
        }

        public override void OnExit()
        {
#if UNITY_EDITOR
            Debug.Log("Exit State: " + $"{GameState.Winning}");
#endif
            winningScreenGameObject.SetActive(false);
        }
    }
}