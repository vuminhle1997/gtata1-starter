using System;
using Score;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Tracks the player's score
    /// </summary>
    public class PointsTracker : MonoBehaviour
    {
        public ScorePoint playerScore;
        private void Awake()
        {
            playerScore = new ScorePoint();
        }
    }
}