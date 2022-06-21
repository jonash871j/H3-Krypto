using XSystem.Security.Cryptography;

namespace Hashing
{
    internal class HashHandler
    {
        public static HashAlgorithm GetHash(string name)
        {
            return name.ToUpper() switch
            {
                "SHA1" => new SHA1Managed(),
                "SHA256" => new SHA256Managed(),
                "SHA512" => new SHA512Managed(),
                _ => null,
            };
        }
        
    }
}
