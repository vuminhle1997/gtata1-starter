using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Score
{
    public class ScorePoint
    {
        private int currentScore;

        public ScorePoint()
        {
            currentScore = 0;
        }

        public void SetCurrentScore(int score)
        {
            currentScore = score;
        }

        public void AddCurrentScore(int score)
        {
            currentScore += score;
        }

        public int GetCurrentScore()
        {
            return currentScore;
        }
    }
}