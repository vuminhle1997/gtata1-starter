using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Persistence;
using UnityEngine;

namespace Score
{
    public class ScorePoint
    {
        public ScorePoint()
        {
            CurrentScore = 0;
            Name = "";
        }
        
        public int CurrentScore
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }

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
    [Serializable]
    public class HighScoreLadder
    {
        public List<HighScore> highScores;

        public HighScoreLadder()
        {
            this.highScores = new List<HighScore>();
        }

        public void AddHighScore(HighScore player)
        {
            highScores.Add(player);
        }
    }
}