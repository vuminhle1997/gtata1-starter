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

        public SettingsOptions()
        {
            _difficulty = Difficulty.Easy;
            _enableSound = true;
            _enableMusic = true;
            _musicLevel = 100f;
            _soundLevel = 100f;
        }
    }
    public class Settings : MonoBehaviour
    {
        public SettingsOptions settingsOptions;
        public static string PATH = "./json/settings.json";

        private void Awake()
        {
            var _settingsOptions = LoadSettings(PATH);
            if (_settingsOptions != null)
            {
                settingsOptions = _settingsOptions;
            }
            else
            {
                var newSettings = new SettingsOptions();
                settingsOptions = newSettings;
            }
        }

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

        public static SettingsOptions LoadSettings(string subPath)
        {
            var fullPath = Path.Combine(Application.persistentDataPath, subPath);
            using (var streamReader = File.OpenText(fullPath))
            {
                var jsonString = streamReader.ReadToEnd();
                return JsonUtility.FromJson<SettingsOptions>(jsonString);
            }
        }
    }
}
