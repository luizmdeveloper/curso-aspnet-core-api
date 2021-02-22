using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Domain.Infraestructure.Security
{
    public class SiginConfigurations
    {
        public SecurityKey Key { get;  }
        public SigningCredentials signingCredentials { get; }

        public SiginConfigurations() 
        {
            using (var provider = new RSACryptoServiceProvider(2048)) 
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            signingCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}
