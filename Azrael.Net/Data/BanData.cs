using System;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Azrael.Net.Data
{    public class BanData
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("reason")]
        public int? Reason { get; set; }

        [JsonPropertyName("proof")]
        public string Proof { get; set; }

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }

        [JsonPropertyName("moderator")]
        public string Moderator { get; set; }

        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        [JsonConstructor]
        public BanData() { }

        public BanData(string UserID, int BanReason, string ProofURL)
        {
            Id = UserID;
            Reason = BanReason;
            Proof = ProofURL;
        }
    }
}
