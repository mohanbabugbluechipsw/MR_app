using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MR_Application_New.Helpers
{
    public static class HelperMethods
    {
        public static bool IsBase64(string str)
        {
            if (string.IsNullOrEmpty(str)) return false;

            // Check for valid base64 string (only allows characters, and length must be multiple of 4)
            var len = str.Length % 4;
            if (len == 0 || len == 2)
            {
                try
                {
                    Convert.FromBase64String(str);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
    }
}
