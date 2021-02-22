using System;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Infraestructure.Helpers
{
    public static class FinanceHelper
    {
       public static string GetSHA1(this string strPlain)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] HashValue, MessageBytes = UE.GetBytes(strPlain);
            SHA1Managed SHhash = new SHA1Managed();
            var strHex = new StringBuilder();

            HashValue = SHhash.ComputeHash(MessageBytes);

            foreach (byte b in HashValue)
            {
                strHex.Append(String.Format("{0:x2}", b));
            }

            return strHex.ToString();
        }
    }
}
