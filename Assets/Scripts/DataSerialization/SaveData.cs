using System;

namespace DataSerialization
{
    [Serializable]
    public class SaveData
    {
        public float playtime = 0f; // Total playtime in seconds
        public DateTime lastPlay = DateTime.Now;
        public string savePoint = "Intro";
        public float[] playerLocation = new float[3]; // 0 = x, 1 = y, 2 = z
        public bool flipped;
    }
}