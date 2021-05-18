using System.Collections.Generic;
using StateMachines;
using UnityEngine;

namespace Handlers.GameHandler
{
    public class LevelSuccessHandler: StateHandler
    {
        [SerializeField] private GameObject levelSuccessScreenGameObject;
        public override void OnEnter(Dictionary<string, object> payload = null)
        {
            Debug.Log("Enter State: " + $"{GameState.LevelSuccess}");
            levelSuccessScreenGameObject.SetActive(true);
        }

        public override void OnExit()
        {
            Debug.Log("Exit State: " + $"{GameState.LevelSuccess}");
            levelSuccessScreenGameObject.SetActive(false);
        }
    }
}