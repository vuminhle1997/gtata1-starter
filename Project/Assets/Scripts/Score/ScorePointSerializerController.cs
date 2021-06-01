using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Score
{
    /// <summary>
    /// Based on the environments, store/load the setting options via binary or json.
    /// If DEV, then Json.
    /// Otherwise, binary.
    /// Responsible for Se- and Deserialization of Ladder Board
    /// </summary>
    public class ScorePointSerializerController: MonoBehaviour
    {
        public HighScoreLadder highScoreLadder;
        public const string PATH = "./json/highscore.json";
        public const string BIN_PATH = "./bin/highscore";

        
        /// <summary>
        /// Loads the ladder abd attach this to highScoreLadder, if one exists.
        /// Otherwise, create a new ladder
        /// </summary>
        private void OnEnable()
        {
            HighScoreLadder _highScoreLadder;
            #if UNITY_EDITOR
                _highScoreLadder = LoadHighScoreLadder(PATH);
            #else
                _highScoreLadder = LoadHighScoreLadder(BIN_PATH);
            #endif
            
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

        #region Serializer/Saver

        /// <summary>
        /// Saves the current loaded ladder.
        /// </summary>
        /// <param name="subPath"></param>
        public void SaveHighScore(string subPath)
        {
#if UNITY_EDITOR
            SaveHighScoreAsJson(subPath);
#else
            SaveHighScoreAsBinary(subPath);
#endif
        }

        private void SaveHighScoreAsJson(string subPath)
        {
            var jsonString = JsonUtility.ToJson(highScoreLadder);
            var fullPath = Path.Combine(Application.persistentDataPath, subPath);
            using var streamWriter = File.CreateText(fullPath);
            streamWriter.Write(jsonString);
        }

        private void SaveHighScoreAsBinary(string subPath)
        {
            var binaryFormatter = new BinaryFormatter();
            var fullPath = Path.Combine(Application.persistentDataPath, subPath);
            using var streamWriter = File.Open(fullPath, FileMode.OpenOrCreate);
            binaryFormatter.Serialize(streamWriter, highScoreLadder);
        }

        #endregion

        #region Deserializer/Loader

        /// <summary>
        /// Loads the ladder in the storage.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static HighScoreLadder LoadHighScoreLadder(string path)
        {
#if UNITY_EDITOR
            return LoadHighScoreLadderFromJson(path);
#else
            return LoadHighScoreLadderFromBinary(path);
#endif
        }

        private static HighScoreLadder LoadHighScoreLadderFromJson(string path)
        {
            var fullPath = Path.Combine(Application.persistentDataPath, path);
            using var streamReader = File.OpenText(fullPath);
            var jsonString = streamReader.ReadToEnd();
            return JsonUtility.FromJson<HighScoreLadder>(jsonString);
        }

        private static HighScoreLadder LoadHighScoreLadderFromBinary(string path)
        {
            var binaryFormatter = new BinaryFormatter();
            var fullPath = Path.Combine(Application.persistentDataPath, path);
            using var streamReader = File.Open(fullPath, FileMode.Open);
            return (HighScoreLadder) binaryFormatter.Deserialize(streamReader);
        }

        #endregion
    }
}