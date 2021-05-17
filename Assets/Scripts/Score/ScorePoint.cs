using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Score
{
    public class ScorePoint
    {
        private int _currentScore;

        public ScorePoint()
        {
            _currentScore = 0;
        }

        public void SetCurrentScore(int score)
        {
            _currentScore = score;
        }

        public void AddCurrentScore(int score)
        {
            _currentScore += score;
        }

        public int GetCurrentScore()
        {
            return _currentScore;
        }
    }
}