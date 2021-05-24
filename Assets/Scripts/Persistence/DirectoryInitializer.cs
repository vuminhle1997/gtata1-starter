using System.IO;
using UnityEngine;

namespace Persistence
{
    public class DirectoryInitializer : MonoBehaviour
    {
        private void Start()
        {
            var jsonDir = Path.Combine(Application.persistentDataPath, "json");
            var binDir = Path.Combine(Application.persistentDataPath, "bin");
            if (!Directory.Exists(jsonDir))
            {
                Directory.CreateDirectory(jsonDir);
            }

            if (!Directory.Exists(binDir))
            {
                Directory.CreateDirectory(binDir);
            }
        }
    }
}
