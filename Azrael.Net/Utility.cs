using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azrael.Net.Data;

namespace Azrael.Net
{
    class Utility
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
    }
}
