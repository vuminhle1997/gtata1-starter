using System;
using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Score
{
    /**
     * Controller to listen on OnScoreChange events
     */
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreTextGUI;
        private int currentScore;
        public int CurrentScore
        {
            get => currentScore;
            set => currentScore = value;
        }
        
        #region Awake Behaviour

        // as default set current score to 0
        private void Awake()
        {
            CurrentScore = 0;
        }

        #endregion
        
        #region OnEnable / OnDisable

        private void OnEnable()
        {
            GlobalEvents.OnScoreChanged += ExecuteScoreChanged;
        }

        private void OnDisable()
        {
            GlobalEvents.OnScoreChanged -= ExecuteScoreChanged;
        }

        #endregion
        
        private void ExecuteScoreChanged(int value)
        {
            // add score change to current score
            // convert current score to string and
            // update score gui text
            
            CurrentScore += value;
            scoreTextGUI.text = CurrentScore.ToString();
        }
    }
}