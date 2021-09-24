using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Azrael.Net.Test
{
    class Utility
    {
        public static string GetToken(string path)
        {
            StreamReader stream = new StreamReader(path);
            return stream.ReadToEnd().Trim();
        }
    }
}
