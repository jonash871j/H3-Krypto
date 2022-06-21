using System.Security.Cryptography;

namespace Hashing
{
    internal static class RandomKeyGenerator
    {
        public static byte[] GenerateRandomKey()
        {
            RNGCryptoServiceProvider random = new();
            byte[] key = new byte[32];
            random.GetBytes(key);
            return key;
        }
    }
}
