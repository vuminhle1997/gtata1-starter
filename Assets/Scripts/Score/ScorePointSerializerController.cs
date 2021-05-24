using System.IO;
using UnityEngine;

namespace Score
{
    public class ScorePointSerializerController: MonoBehaviour
    {
        public HighScoreLadder highScoreLadder;
            
#if UNITY_EDITOR
        public const string Path = "./json/highscore.json";
#elif UNITY_STANDALONE
        public const string Path = "./json/highscore.json";
#endif
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

        public void AddPlayerToLadder(HighScore player)
        {
            highScoreLadder.AddHighScore(player);
        }
        
        public void SaveSettings(string subPath)
        {
            var jsonString = JsonUtility.ToJson(highScoreLadder);
            var fullPath = System.IO.Path.Combine(Application.persistentDataPath, subPath);
            using var streamWriter = File.CreateText(fullPath);
            streamWriter.Write(jsonString);
                
            Debug.Log("Saved settings successful");
        }

        private static HighScoreLadder LoadHighScoreLadder(string path)
        {
            var fullPath = System.IO.Path.Combine(Application.persistentDataPath, path);
            using var streamReader = File.OpenText(fullPath);
            var jsonString = streamReader.ReadToEnd();
            return JsonUtility.FromJson<HighScoreLadder>(jsonString);
        }
    }
}