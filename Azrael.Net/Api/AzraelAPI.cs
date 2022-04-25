using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using Azrael.Net.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Azrael.Net.Api
{
    public class AzraelAPI
    {
        public static string BaseURL = @"https://api.azrael.gg/v5/";

        // https://api.azrael.gg/api/v3/bans/check/:id
        public static async Task<bool> CheckBan(string UserID, string ApiToken)
        {
            HttpClient Client = Utility.GetHttpClient(BaseURL, ApiToken);
            HttpResponseMessage _apiResponse = await Client.GetAsync("bans/check/" + UserID);
            BanRecord _apiBan = JsonSerializer.Deserialize<BanRecord>(await _apiResponse.Content.ReadAsStringAsync());
            Utility.ProcessStatusCode(_apiBan);
            if (_apiBan.Banned)
            {
                return true;
            }
            return false;
        }

        // https://api.azrael.gg/api/v3/bans/get/:id
        public static async Task<BanRecord> GetBan(string UserID, string ApiToken)
        {
            HttpClient Client = Utility.GetHttpClient(BaseURL, ApiToken);
            HttpResponseMessage _apiResponse = await Client.GetAsync("bans/get/" + UserID);
            BanRecord ApiBan = JsonSerializer.Deserialize<BanRecord>(await _apiResponse.Content.ReadAsStringAsync());
            Utility.ProcessStatusCode(ApiBan);
            return ApiBan;            
        }

        // https://api.azrael.gg/api/v3/bans/add/
        public static async Task<BanRecord> AddBan(string UserID, string ApiToken, int BanReason, string Proof)
        {
            HttpClient Client = Utility.GetHttpClient(BaseURL, ApiToken);
            var _banJson = JsonSerializer.Serialize(new BanData(UserID, BanReason, Proof));
            HttpResponseMessage _apiResponse = await Client.PostAsync("bans/add/", new StringContent(_banJson, Encoding.UTF8, "application/json"));
            string test = await _apiResponse.Content.ReadAsStringAsync();
            BanRecord ApiBan = JsonSerializer.Deserialize<BanRecord>(await _apiResponse.Content.ReadAsStringAsync());
            Utility.ProcessStatusCode(ApiBan);
            return ApiBan;
        }

        // https://api.azrael.gg/api/v3/bans/remove/:id
        public static async Task<bool> DeleteBan(string UserID, string ApiToken, string Reason)
        {
            HttpClient Client = Utility.GetHttpClient(BaseURL, ApiToken);
            var reasonBody = new[] { new { Reason = Reason } };
            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Content = JsonContent.Create(Reason),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(BaseURL + "bans/remove/" + UserID)
            };
            HttpResponseMessage _apiResponse = await Client.SendAsync(requestMessage);
            BanRecord _apiBan = JsonSerializer.Deserialize<BanRecord>(await _apiResponse.Content.ReadAsStringAsync());
            Utility.ProcessStatusCode(_apiBan);
            if (_apiBan.Status == 200)
                return true;
            return false;
        }
    }
}
