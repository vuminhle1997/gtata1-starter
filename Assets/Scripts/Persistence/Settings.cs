using System;
using System.IO;
using UnityEngine;

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

namespace Persistence
{
    [System.Serializable]
    public class SettingsOptions
    {
        public Difficulty _difficulty = Difficulty.Easy;
        public bool _enableSound = true;
        public bool _enableMusic = true;
        public float _musicLevel = 100f;
        public float _soundLevel = 100f;
    }
    public class Settings : MonoBehaviour
    {
        public SettingsOptions settingsOptions;
        public static string PATH = "settings.json";

        public static void SaveSettings(SettingsOptions options, string subPath)
        {
            var jsonString = JsonUtility.ToJson(options);
            var fullPath = Path.Combine(Application.persistentDataPath, subPath);
            using (var streamWriter = File.CreateText(fullPath))
            {
                streamWriter.Write(jsonString);
                
                Debug.Log("Saved settings successful");
            }
        }
    }
}
