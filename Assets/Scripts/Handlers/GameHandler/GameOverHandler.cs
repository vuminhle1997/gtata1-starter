using System.Collections.Generic;
using StateMachines;
using UnityEngine;

namespace Handlers.GameHandler
{
    /// <summary>
    /// Toggles the game over screen with score and input field.
    /// </summary>
    public class GameOverHandler: StateHandler
    {
        [SerializeField] private GameObject gameOverScreenGameObject;
        public override void OnEnter(Dictionary<string, object> payload = null)
        {
            Debug.Log("Enter State: " + $"{GameState.GameOver}");
            gameOverScreenGameObject.SetActive(true);
        }

        public override void OnExit()
        {
            Debug.Log("Exit State: " + $"{GameState.GameOver}");
            gameOverScreenGameObject.SetActive(false);
        }
    }
}