using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Persistence;
using UnityEngine;

namespace Score
{
    [System.Serializable]
    public class ScorePoint
    {
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

    [System.Serializable]
    public class HighScoreLadder
    {
        public List<ScorePoint> highScores;

        public HighScoreLadder()
        {
            this.highScores = new List<ScorePoint>();
        }
    }

    public static class ScorePointSerializer
    {
        public const string Path = "./json/highscore.json";

        public static void AddPlayerToLadder(HighScoreLadder highScoreLadder, ScorePoint player)
        {
            highScoreLadder.highScores.Add(player);
        }
        
        public static void SaveSettings(HighScoreLadder highScore, string subPath)
        {
            var jsonString = JsonUtility.ToJson(highScore);
            var fullPath = System.IO.Path.Combine(Application.persistentDataPath, subPath);
            using var streamWriter = File.CreateText(fullPath);
            streamWriter.Write(jsonString);
                
            Debug.Log("Saved settings successful");
        }

        public static HighScoreLadder LoadHighScoreLadder(string path)
        {
            var fullPath = System.IO.Path.Combine(Application.persistentDataPath, path);
            using var streamReader = File.OpenText(fullPath);
            var jsonString = streamReader.ReadToEnd();
            return JsonUtility.FromJson<HighScoreLadder>(jsonString);
        }
    }
}