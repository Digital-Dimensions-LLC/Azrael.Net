using System;
using System.Text.Json.Serialization;

namespace Azrael.Net.Data
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Ban>(myJsonResponse); 
    public class BanRecord
    {
        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("banned")]
        public bool Banned { get; set; }

        [JsonPropertyName("bandata")]
        public BanData BanData { get; set; }
    }
    public class BanData
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("reason")]
        public string Reason { get; set; }

        [JsonPropertyName("proof")]
        public string Proof { get; set; }

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }

        [JsonPropertyName("moderator")]
        public string Moderator { get; set; }

        [JsonPropertyName("notes")]
        public string Notes { get; set; }

        public BanData(string UserID, string BanReason, string ProofURL)
        {
            Id = UserID;
            Reason = BanReason;
            Proof = ProofURL;
        }
    }
}
