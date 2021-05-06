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
    public class PersistenceHandler<TData>
    {

        #region Save Data / Serialization

        public static void SaveData(TData data, string subPath)
        {
#if UNITY_EDITOR
            
            SaveDataAsJson(data, subPath);
        
#endif
#if UNITY_STANDALONE
      
            SaveDataAsBinary(data, subPath);
            
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

        public static void LoadData(TData data, string subPath)
        {
#if UNITY_EDITOR
            
            LoadDataAsJson(subPath);
        
#endif
#if UNITY_STANDALONE
      
            LoadDataAsBinary(subPath);
            
#endif
        }
        
        private static TData LoadDataAsJson(string subPath)
        {
            var fullPath = Path.Combine(Application.persistentDataPath, subPath);
            using var streamWriter = File.OpenText(fullPath);
            var jsonString = streamWriter.ReadToEnd();
            return JsonUtility.FromJson<TData>(jsonString);
        }

        private static TData LoadDataAsBinary(string subPath)
        {
            var binaryFormatter = new BinaryFormatter();
            var fullPath = Path.Combine(Application.persistentDataPath, subPath);
            using var streamWriter = File.Open(fullPath, FileMode.Open);
            return (TData) binaryFormatter.Deserialize(streamWriter);
        }

        #endregion
    }
}