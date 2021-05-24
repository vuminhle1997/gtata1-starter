using System.IO;
using UnityEngine;

namespace Score
{
    /// <summary>
    /// Class for storing the player's score into a ladder.
    /// </summary>
    public class ScorePointSerializerController: MonoBehaviour
    {
        public HighScoreLadder highScoreLadder;
            
#if UNITY_EDITOR
        public const string Path = "./json/highscore.json";
#elif UNITY_STANDALONE
        public const string Path = "./json/highscore.json";
#endif
        
        /// <summary>
        /// Loads the ladder abd attach this to highScoreLadder, if one exists.
        /// Otherwise, create a new ladder
        /// </summary>
        private void OnEnable()
        {
            var _highScoreLadder = LoadHighScoreLadder(Path);
            if (_highScoreLadder != null)
            {
                highScoreLadder = _highScoreLadder;
            }
            else
            {
                highScoreLadder = new HighScoreLadder();
            }
        }

        /// <summary>
        /// Adds the player's score with name to the current loaded ladder.
        /// </summary>
        /// <param name="player"></param>
        public void AddPlayerToLadder(HighScore player)
        {
            highScoreLadder.AddHighScore(player);
        }
        
        /// <summary>
        /// Saves the current loaded ladder.
        /// </summary>
        /// <param name="subPath"></param>
        public void SaveSettings(string subPath)
        {
            var jsonString = JsonUtility.ToJson(highScoreLadder);
            var fullPath = System.IO.Path.Combine(Application.persistentDataPath, subPath);
            using var streamWriter = File.CreateText(fullPath);
            streamWriter.Write(jsonString);
                
            Debug.Log("Saved settings successful"); 
        }

        /// <summary>
        /// Loads the ladder in the storage.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static HighScoreLadder LoadHighScoreLadder(string path)
        {
            var fullPath = System.IO.Path.Combine(Application.persistentDataPath, path);
            using var streamReader = File.OpenText(fullPath);
            var jsonString = streamReader.ReadToEnd();
            return JsonUtility.FromJson<HighScoreLadder>(jsonString);
        }
    }
}