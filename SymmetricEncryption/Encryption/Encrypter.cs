using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SymmetricEncryption.Encryption
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
    }
}
