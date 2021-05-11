using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Persistence
{
    /**
     * Serialize or deserialize data of type TData. The persistence is handled via saving/loading to Json files
     * (for development), or to Binary (for production)
     * <typeparam name="TData">Data to Serialize / Deserialize</typeparam>
     */
    public class PersistenceHandler<TData> where TData : new()
    {
        // persistent application data path
        private static readonly string _persistentDataPath = Application.persistentDataPath;
        /**
         * JSON directory path
         */
        private static string _jsonDirPath;
        /**
         * Binary directory path
         */
        private static string _binaryDirPath;

        /**
         * Setup directory structure to divide between json files and binary files 
         */
        public static void SetupDirectories()
        {
            _jsonDirPath = Path.Combine(_persistentDataPath, "json");
            _binaryDirPath = Path.Combine(_persistentDataPath, "binary");
            
            // if json dir does not exist yet, create it
            if (!Directory.Exists(_jsonDirPath))
            {
                Directory.CreateDirectory(_jsonDirPath);
            }
            // if binary dir does not exist yet, create it
            if (!Directory.Exists(_binaryDirPath))
            {
                Directory.CreateDirectory(_binaryDirPath);
            }
        }

        #region Save Data / Serialization

        /**
         * Save the data of type TData at sub-path given. For development builds JSON format is used to store persistent
         * Data. For standalone builds the data will be saved as a binary format.
         * <param name="data">data to save of type TData</param>
         * <param name="subPath">string sub-path of the data to save at</param>
         */
        public static void SaveData(TData data, string subPath)
        {
#if UNITY_EDITOR
            SaveDataAsJson(data, $"{_jsonDirPath}/{subPath}");
#elif UNITY_STANDALONE
            SaveDataAsBinary(data, $"{_binaryDirPath}/{subPath}");
#endif
        }
        
        private static void SaveDataAsJson(TData data, string subPath)
        {
            var jsonString = JsonUtility.ToJson(data);
            var fullPath = Path.Combine(Application.persistentDataPath, subPath);
            using var streamWriter = File.CreateText(fullPath);
            streamWriter.Write(jsonString);
        }

        private static void SaveDataAsBinary(TData data, string subPath)
        {
            var binaryFormatter = new BinaryFormatter();
            var fullPath = Path.Combine(Application.persistentDataPath, subPath);
            using var streamWriter = File.Open(fullPath, FileMode.OpenOrCreate);
            binaryFormatter.Serialize(streamWriter, data);
        }

        #endregion

        #region Load Data / De-Serialization

        /**
         * Load persistent data into the application at runtime. If the application is run as development,
         * the persistent data will be loaded from json. Else it will use a binary formatter. If there is no
         * persistent data found to be loaded a new object of type TData will be returned.
         * <param name="subPath">Sub-Path name of object to load</param>
         * <returns>TData object</returns>
         */
        public static TData LoadData(string subPath)
        {
#if UNITY_EDITOR
            
            return LoadDataAsJson($"{_jsonDirPath}/{subPath}");
        
#endif
#if UNITY_STANDALONE
      
            return LoadDataAsBinary($"{_binaryDirPath}/{subPath}");
            
#endif
        }
        
        private static TData LoadDataAsJson(string subPath)
        {
            try
            {
                var fullPath = Path.Combine(Application.persistentDataPath, subPath);
                using var streamWriter = File.OpenText(fullPath);
                var jsonString = streamWriter.ReadToEnd();
                return JsonUtility.FromJson<TData>(jsonString);
            }
            catch
            {
                return new TData();
            }
        }

        private static TData LoadDataAsBinary(string subPath)
        {
            try
            {
                var binaryFormatter = new BinaryFormatter();
                var fullPath = Path.Combine(Application.persistentDataPath, subPath);
                using var streamWriter = File.Open(fullPath, FileMode.Open);
                return (TData) binaryFormatter.Deserialize(streamWriter);
            }
            catch
            {
                return new TData();
            }
        }

        #endregion
    }
}