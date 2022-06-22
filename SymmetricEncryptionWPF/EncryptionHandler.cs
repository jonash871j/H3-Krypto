using System.Security.Cryptography;

namespace SymmetricEncryptionWPF
{
    internal class EncryptionHandler
    {
        public static SymmetricAlgorithm GetSymmetricAlgorithm(string name)
        {
            return name.ToUpper() switch
            {
                "DES" => DES.Create(),
                "TripleDES" => TripleDES.Create(),
                "RC2" => RC2.Create(),
                "Rijndael" => Rijndael.Create(),
                "AES" => Aes.Create(),
                _ => DES.Create(),
            };
        }
        
    }
}
