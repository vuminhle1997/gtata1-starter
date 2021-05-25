using System;
using System.Collections.Generic;

namespace Score
{
    /// <summary>
    /// Class for containing the playername and current score, needed for other components
    /// </summary>
    public class ScorePoint
    {
        public ScorePoint()
        {
            CurrentScore = 0;
        }
        
        public int CurrentScore
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Only needed for saving/loading HighScore. Same as HighScore.
    /// I don't know, but I needed to make another class for serialization.
    /// </summary>
    [Serializable]
    public class HighScore
    {
        public int score;
        public string name;

        public HighScore(int score, string name)
        {
            this.score = score;
            this.name = name;
        }
    }
    
    /// <summary>
    /// High Score ladder.
    /// Contains list of scores (score and username of player)
    /// </summary>
    [Serializable]
    public class HighScoreLadder
    {
        public List<HighScore> highScores;

        public HighScoreLadder()
        {
            this.highScores = new List<HighScore>();
        }

        /// <summary>
        /// Adds score and player name to ladder
        /// </summary>
        /// <param name="player"></param>
        public void AddHighScore(HighScore player)
        {
            highScores.Add(player);
        }
    }
}