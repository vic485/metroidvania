using System;

namespace DataSerialization
{
    public class SaveData
    {
        public float playtime { get; set; } = 0f; // Total playtime in seconds
        public DateTime lastPlay { get; set; } = DateTime.Now;
        public string savePoint { get; set; } = "Intro";
        public float[] playerLocation { get; set; } = new float[3]; // 0 = x, 1 = y, 2 = z
        public bool flipped { get; set; }
    }
}