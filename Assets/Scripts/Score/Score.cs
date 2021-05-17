using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Score
{
    public class Score
    {
        private int currentScore;

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