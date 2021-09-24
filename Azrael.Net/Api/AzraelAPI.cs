using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Azrael.Net.Data;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Azrael.Net.Api
{
    public class AzraelAPI
    {
        public static string BaseURL = @"https://azrael.gg/api/v2/";

        // https://azrael.gg/api/v2/bans/check/:id
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

        // https://azrael.gg/api/v2/bans/get/:id
        public static async Task<BanRecord> GetBan(string UserID, string ApiToken)
        {
            HttpClient Client = Utility.GetHttpClient(BaseURL, ApiToken);
            HttpResponseMessage _apiResponse = await Client.GetAsync("bans/get/" + UserID);
            BanRecord ApiBan = JsonSerializer.Deserialize<BanRecord>(await _apiResponse.Content.ReadAsStringAsync());
            Utility.ProcessStatusCode(ApiBan);
            return ApiBan;            
        }

        // https://azrael.gg/api/v2/bans/add/
        public static async Task<BanRecord> AddBan(string UserID, string ApiToken, string BanReason, string Proof)
        {
            HttpClient Client = Utility.GetHttpClient(BaseURL, ApiToken);
            var _banJson = JsonSerializer.Serialize(new BanData(UserID, BanReason, Proof));
            HttpResponseMessage _apiResponse = await Client.PostAsync("bans/add/", new StringContent(_banJson));
            BanRecord ApiBan = JsonSerializer.Deserialize<BanRecord>(await _apiResponse.Content.ReadAsStringAsync());
            Utility.ProcessStatusCode(ApiBan);
            return ApiBan;
        }

        // https://azrael.gg/api/v2/bans/remove/:id
        public static async Task<bool> DeleteBan(string UserID, string ApiToken)
        {
            HttpClient Client = Utility.GetHttpClient(BaseURL, ApiToken);
            HttpResponseMessage _apiResponse = await Client.DeleteAsync("bans/remove/" + UserID);
            BanRecord _apiBan = JsonSerializer.Deserialize<BanRecord>(await _apiResponse.Content.ReadAsStringAsync());
            Utility.ProcessStatusCode(_apiBan);
            if (_apiBan.Status == 200)
                return true;
            return false;
        }
    }
}
