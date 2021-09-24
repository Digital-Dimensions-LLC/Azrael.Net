using System;
using System.Text.Json.Serialization;

namespace Azrael.Net.Data
{
    public class BanRecord
    {
        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("banned")]
        public bool Banned { get; set; }

        [JsonPropertyName("bandata")]
        public BanData BanData { get; set; }
    }
    
}
