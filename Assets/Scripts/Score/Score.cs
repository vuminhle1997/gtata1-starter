using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Score
{
    [Serializable]
    public class Score
    {
        [SerializeField] private int currentScore;

        public int CurrentScore
        {
            get => currentScore;
            set => currentScore = value;
        }

        #region Constructors

        public Score()
        {
            currentScore = 0;
        }

        public Score(int value)
        {
            currentScore = value;
        }

        #endregion

        public override string ToString()
        {
            return CurrentScore.ToString();
        }
    }
}