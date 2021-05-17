using System.Collections.Generic;
using StateMachines;
using UnityEngine;

namespace Handlers
{
    public class HighScoreHandler: StateHandler
    {
        [SerializeField] private MenuStateMachine menuStateMachine;
        public override void OnEnter(Dictionary<string, object> payload = null)
        {
            Debug.Log("Enter State: " + $"{MenuState.HighScore}");
            var highScorePanel = menuStateMachine.GetGameObject(MenuState.HighScore);
            menuStateMachine.EnableGameObject(highScorePanel);
        }

        public override void OnExit()
        {
            Debug.Log("Exit State: " + $"{MenuState.HighScore}");
            var highScorePanel = menuStateMachine.GetGameObject(MenuState.HighScore);
            menuStateMachine.DisableGameObject(highScorePanel);
        }
    }
}