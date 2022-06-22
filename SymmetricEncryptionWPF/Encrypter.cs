using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SymmetricEncryptionWPF
{
    internal class Encrypter
    {
        public static byte[] Encrypt(SymmetricAlgorithm symmetricAlgorithm, string text)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            MemoryStream ms = new();
            CryptoStream cs = new(ms, symmetricAlgorithm.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(buffer);
            cs.Close();
            return ms.ToArray();
        }

        public static string Decrypt(SymmetricAlgorithm symmetricAlgorithm, byte[] bytes)
        {
            byte[] buffer = new byte[bytes.Length];
            MemoryStream ms = new(bytes);
            CryptoStream cs = new(ms, symmetricAlgorithm.CreateDecryptor(), CryptoStreamMode.Read);
            cs.Read(buffer);
            cs.Close();
            return Encoding.ASCII.GetString(buffer);
        }
    }
}
