using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Azrael.Net.Data;

namespace Azrael.Net
{
    public class Utility
    {
        public static void ProcessStatusCode(BanRecord response)
        {
            if (response.Status == 200)
                return;
            if (response.Status == 400)
                throw new ArgumentException("The user ID provided is invalid.");
            if (response.Status == 401)
                throw new Exception("The server returned status 401: Unauthorized");
            if (response.Status == 403)
                throw new UnauthorizedAccessException("The server returned status 403: Authorization token invalid");
            if (response.Status == 404)
                throw new Exception("The server returned status 404: URL not found");
            if (response.Status == 500)
                throw new Exception("The server returned status 500: Internal Server Error");
        }

        public static HttpClient GetHttpClient(string BaseURL,string ApiToken)
        {
            HttpClient ApiRequest = new HttpClient();
            ApiRequest.BaseAddress = new Uri(BaseURL);

            // TLS 1.2 due to https://docs.microsoft.com/en-us/windows/win32/secauthn/protocols-in-tls-ssl--schannel-ssp-#tls-protocol-version-support
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            ApiRequest.DefaultRequestHeaders.Accept.Clear();
            ApiRequest.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ApiRequest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(ApiToken);
            return ApiRequest;
        }
    }
}
