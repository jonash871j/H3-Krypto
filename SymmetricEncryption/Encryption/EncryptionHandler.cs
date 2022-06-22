
using System.Security.Cryptography;

namespace SymmetricEncryption.Encryption
{
    internal class EncryptionHandler
    {
        public static SymmetricAlgorithm GetSymmetricAlgorithm(string name)
        {
            return name.ToUpper() switch
            {
                "DES" => DES.Create(),
                "TripleDES" => TripleDES.Create(),
                _ => null,
            };
        }
        
    }
}
