using System;

namespace Encrypter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string encrypted = Encrypter.Encrypt("Hello World this is a test");
            Console.WriteLine("Encrypted text: " + encrypted);
            string decrypted = Encrypter.Decrypt(encrypted);
            Console.WriteLine("Decrypted text: " + decrypted);
        }
    }
}
