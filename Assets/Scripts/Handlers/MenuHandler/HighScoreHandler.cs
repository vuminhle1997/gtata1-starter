using System.Collections.Generic;
using Score;
using StateMachines;
using UnityEngine;
using Utils;

namespace Handlers
{
    public class HighScoreHandler: StateHandler
    {
        [SerializeField] private ScorePointSerializerController scorePointSerializerController;
        [SerializeField] private GameObject highScoreGameObject;
        public List<HighScore> highScores;
        public override void OnEnter(Dictionary<string, object> payload = null)
        {
            Debug.Log("Enter State: " + $"{MenuState.HighScore}");
            // var highScorePanel = menuStateMachine.GetGameObject(MenuState.HighScore);
            // menuStateMachine.EnableGameObject(highScorePanel);
            highScoreGameObject.SetActive(true);
            var ladder = scorePointSerializerController.highScoreLadder;
            var _highScores = SortHighScores.GetDescendingHighScores(ladder);
            highScores = _highScores;
        }

        public override void OnExit()
        {
            Debug.Log("Exit State: " + $"{MenuState.HighScore}");
            // var highScorePanel = menuStateMachine.GetGameObject(MenuState.HighScore);
            // menuStateMachine.DisableGameObject(highScorePanel);
            highScores = null;
            highScoreGameObject.SetActive(false);
        }
    }
}