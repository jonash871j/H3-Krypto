using SshNet.Security.Cryptography;

namespace Hashing
{
    internal static class HMACHandler
    {
        public static HMAC GetHMAC(string name)
        {
            byte[] key = RandomKeyGenerator.GenerateRandomKey();
            return name.ToUpper() switch
            {
                "SHA1" => new HMACSHA1(key),
                "MD5" => new HMACMD5(key),
                "RIPEMD" => new HMACRIPEMD160(key),
                "SHA256" => new HMACSHA256(key),
                "SHA384" => new HMACSHA384(key),
                "SHA512" => new HMACSHA512(key),
                _ => null,
            };
        }
    }
}
