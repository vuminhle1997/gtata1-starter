using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
    
    /// <summary>
    /// Based on the environments, store/load the setting options via binary or json.
    /// If DEV, then Json.
    /// Otherwise, binary. 
    /// </summary>
    public class Settings : MonoBehaviour
    {
        public SettingsOptions settingsOptions;
        public const string PATH = "./json/settings.json";
        public const string BIN_PATH = "./bin/settings";

        private void Awake()
        {
            SettingsOptions _settingsOptions;
            
            #if UNITY_EDITOR
                _settingsOptions = LoadSettings(PATH);  
            #else
                _settingsOptions = LoadSettings(BIN_PATH);
            #endif

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

        #region Serializer/Saver

        public static void SaveSettings(SettingsOptions options, string subPath)
        {
#if UNITY_EDITOR
            SaveSettingsAsJson(options, subPath);
#else
            SaveSettingsAsBinary(options, subPath);
#endif
        }

        private static void SaveSettingsAsJson(SettingsOptions options, string subPath)
        {
            var jsonString = JsonUtility.ToJson(options);
            var fullPath = Path.Combine(Application.persistentDataPath, subPath);
            using var streamWriter = File.CreateText(fullPath);
            streamWriter.Write(jsonString);
        }

        private static void SaveSettingsAsBinary(SettingsOptions options, string subPath)
        {
            var binaryFormatter = new BinaryFormatter();
            var fullPath = Path.Combine(Application.persistentDataPath, subPath);
            using var streamWriter = File.Open(fullPath, FileMode.OpenOrCreate);
            binaryFormatter.Serialize(streamWriter, options);
        }

        #endregion


        #region Deserializer/Loader

        public static SettingsOptions LoadSettings(string subPath)
        {
#if UNITY_EDITOR
            return LoadSettingsFromJson(subPath);
#else
            return LoadSettingsFromBinary(subPath);
#endif
        }
        
        private static SettingsOptions LoadSettingsFromJson(string subPath)
        {
            var fullPath = Path.Combine(Application.persistentDataPath, subPath);
            using var streamReader = File.OpenText(fullPath);
            var jsonString = streamReader.ReadToEnd();
            return JsonUtility.FromJson<SettingsOptions>(jsonString);
        }

        private static SettingsOptions LoadSettingsFromBinary(string subPath)
        {
            var binaryFormatter = new BinaryFormatter();
            var fullPath = Path.Combine(Application.persistentDataPath, subPath);
            using var streamReader = File.Open(fullPath, FileMode.Open);
            return (SettingsOptions) binaryFormatter.Deserialize(streamReader);
        }

        #endregion
    }
}
