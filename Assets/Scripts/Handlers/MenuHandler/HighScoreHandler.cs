using System.Collections.Generic;
using Score;
using StateMachines;
using UnityEngine;
using Utils;

namespace Handlers.MenuHandler
{
    /// <summary>
    /// Toggles the high score ladder screen.
    /// Loads the ladder, it this state is entered.
    /// </summary>
    public class HighScoreHandler: StateHandler
    {
        [SerializeField] private ScorePointSerializerController scorePointSerializerController;
        [SerializeField] private GameObject highScoreGameObject;
        public List<HighScore> highScores;
        
        /// <summary>
        /// Loads the scores in enter.
        /// </summary>
        /// <param name="payload"></param>
        public override void OnEnter(Dictionary<string, object> payload = null)
        {
            Debug.Log("Enter State: " + $"{MenuState.HighScore}");
            highScoreGameObject.SetActive(true);
            var ladder = scorePointSerializerController.highScoreLadder;
            var _highScores = SortHighScores.GetDescendingHighScores(ladder);
            highScores = _highScores;
        }

        /// <summary>
        /// Remove the score on exit.
        /// </summary>
        public override void OnExit()
        {
            Debug.Log("Exit State: " + $"{MenuState.HighScore}");
            highScores = null;
            highScoreGameObject.SetActive(false);
        }
    }
}