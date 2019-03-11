using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace DataSerialization
{
    public static class DataSerializer
    {
        public static SaveData LoadGame(int saveSlot)
        {
            var filePath = Path.Combine(Application.persistentDataPath, $"save{saveSlot}.sav");

            if (!File.Exists(filePath))
                return null;

            try
            {
                var dataArray = Convert.FromBase64String(File.ReadAllText(filePath));
                using (var dataStream = new MemoryStream(dataArray))
                    return new BinaryFormatter().Deserialize(dataStream) as SaveData;
            }
            catch
            {
                // TODO: Message to user
                return null;
            }
        }
        
        public static void SaveGame(int saveSlot, SaveData data)
        {
            var filePath = Path.Combine(Application.persistentDataPath, $"save{saveSlot}.sav");
            
            // Check and create backup
            if (File.Exists(filePath))
            {
                var backupPath = Path.Combine(Application.persistentDataPath, $"save{saveSlot}.sav.bkp");
                
                if (File.Exists(backupPath))
                    File.Delete(backupPath);
                
                File.Move(filePath, backupPath);
            }

            using (var dataStream = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(dataStream, data);
                File.WriteAllText(filePath, Convert.ToBase64String(dataStream.ToArray()));
            }
        }
    }
}
