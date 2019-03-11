using System;
using System.IO;
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
                return JsonConvert.DeserializeObject<SaveData>(Encoding.UTF8.GetString(dataArray));
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

            var dataString = JsonConvert.SerializeObject(data, Formatting.Indented);
            //File.WriteAllText(Path.Combine(Application.dataPath, "plaintextSave.json"), dataString);
            var dataArray = Encoding.UTF8.GetBytes(dataString);
            File.WriteAllText(filePath, Convert.ToBase64String(dataArray));
        }
    }
}
