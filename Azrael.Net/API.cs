using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Azrael.Net.Data;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Azrael.Net
{
    public class API
    {
        public static string BaseURL = @"https://azrael.gg/v2/api/";

        // https://azrael.gg/v2/api/bans/check/:id
        public static async Task<bool> CheckBan(ulong UserID, string APIKey)
        {
            HttpClient _apiRequest = new HttpClient();
            _apiRequest.BaseAddress = new Uri(BaseURL);
            _apiRequest.DefaultRequestHeaders.Accept.Clear();
            _apiRequest.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiRequest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", APIKey);
            HttpResponseMessage _apiResponse = await _apiRequest.GetAsync("bans/check/" + UserID);
            BanRecord _apiBan = JsonSerializer.Deserialize<BanRecord>(await _apiResponse.Content.ReadAsStringAsync());
            Utility.ProcessStatusCode(_apiBan);
            if (_apiBan.Banned)
                return true;
            return false;
        }

        // https://azrael.gg/api/v2/bans/get/:id
        public static async Task<BanRecord> GetBan(ulong UserID, string APIKey)
        {
            HttpClient _apiRequest = new HttpClient();
            _apiRequest.BaseAddress = new Uri(BaseURL);
            _apiRequest.DefaultRequestHeaders.Accept.Clear();
            _apiRequest.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiRequest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", APIKey);
            HttpResponseMessage _apiResponse = await _apiRequest.GetAsync("bans/get/" + UserID);
            BanRecord ApiBan = JsonSerializer.Deserialize<BanRecord>(await _apiResponse.Content.ReadAsStringAsync());
            Utility.ProcessStatusCode(ApiBan);
            return ApiBan;
            
        }
        // https://azrael.gg/api/v2/bans/add/
        public static async Task<BanRecord> AddBan(string UserID, string APIKey, string BanReason, string Proof)
        {
            HttpClient _apiRequest = new HttpClient();
            _apiRequest.BaseAddress = new Uri(BaseURL);
            _apiRequest.DefaultRequestHeaders.Accept.Clear();
            _apiRequest.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiRequest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", APIKey);
            var _banJson = JsonSerializer.Serialize(new BanData(UserID, BanReason, Proof));
            HttpResponseMessage _apiResponse = await _apiRequest.PostAsync("bans/add/", new StringContent(_banJson));
            BanRecord ApiBan = JsonSerializer.Deserialize<BanRecord>(await _apiResponse.Content.ReadAsStringAsync());
            Utility.ProcessStatusCode(ApiBan);
            return ApiBan;
        }
        // https://azrael.gg/api/v2/bans/remove/:id
        public static async Task<bool> DeleteBan(ulong UserID, string APIKey)
        {
            HttpClient _apiRequest = new HttpClient();
            _apiRequest.BaseAddress = new Uri(BaseURL);
            _apiRequest.DefaultRequestHeaders.Accept.Clear();
            _apiRequest.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiRequest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", APIKey);
            HttpResponseMessage _apiResponse = await _apiRequest.DeleteAsync("bans/remove/" + UserID);
            BanRecord _apiBan = JsonSerializer.Deserialize<BanRecord>(await _apiResponse.Content.ReadAsStringAsync());
            Utility.ProcessStatusCode(_apiBan);
            if (_apiBan.Status == 200)
                return true;
            return false;
        }
    }
}
