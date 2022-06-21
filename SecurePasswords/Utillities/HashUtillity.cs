using System.Security.Cryptography;
using System.Text;

namespace SecurePasswords.Utillity
{
    internal static class HashUtillity
    {
        public static byte[] GenerateSalt()
        {
            RNGCryptoServiceProvider randomNumberGenerator = new RNGCryptoServiceProvider();
            byte[] randomNumber = new byte[32];
            randomNumberGenerator.GetBytes(randomNumber);

            return randomNumber;
        }

        public static byte[] HashText(string toBeHashed, byte[] salt, int numberOfRounds)
        {
            Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(toBeHashed), salt, numberOfRounds);
            return rfc2898.GetBytes(32);
        }

        public static bool CompareHashes(byte[] hash1, byte[] hash2)
        {
            if (hash1.Length != hash2.Length)
            {
                return false;
            }

            for (int i = 0; i < hash1.Length; i++)
            {
                if (hash1[i] != hash2[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
