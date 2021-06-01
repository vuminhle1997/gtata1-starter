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
#if UNITY_EDITOR
            Debug.Log("Enter State: " + $"{GameState.LevelSuccess}");
#endif
            levelSuccessScreenGameObject.SetActive(true);
        }

        public override void OnExit()
        {
#if UNITY_EDITOR
            Debug.Log("Exit State: " + $"{GameState.LevelSuccess}");
#endif
            levelSuccessScreenGameObject.SetActive(false);
        }
    }
}