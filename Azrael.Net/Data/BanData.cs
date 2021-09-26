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

        [JsonIgnore]
        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }

        [JsonIgnore]
        [JsonPropertyName("moderator")]
        public string Moderator { get; set; }

        [JsonIgnore]
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

        public static string GetBanRequest(BanData data)
        {
            BanData good = new BanData(data.Id, 3, data.Proof);
            return JsonSerializer.Serialize<BanData>(good);
        }
    }
}
