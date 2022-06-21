using SshNet.Security.Cryptography;
using System;
using System.Diagnostics;
using System.Text;
using XSystem.Security.Cryptography;

namespace Hashing
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Press any key to start");
            while (true)
            {
                Console.ReadKey();
                Console.Clear();
                Console.Write("Text to hash: ");
                string text = Console.ReadLine();

                Console.Write("Do you want to use HMAC? Press Y OR N : ");
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();

                if (consoleKeyInfo.Key == ConsoleKey.Y)
                {
                    HMACView(text);
                }
                else
                {
                    HashView(text);
                }
            }
        }

        static void HMACView(string text)
        {
            Console.WriteLine("\nUsing HMAC");

            Console.WriteLine("- SHA1");
            Console.WriteLine("- MD5");
            Console.WriteLine("- RIPEMD");
            Console.WriteLine("- SHA256");
            Console.WriteLine("- SHA384");
            Console.WriteLine("- SHA512");
            Console.Write("Algorithm to use: ");
            string algorithmName = Console.ReadLine();

            HMAC hMAC = HMACHandler.GetHMAC(algorithmName);
            if (hMAC == null)
            {
                Console.WriteLine("Invalid algorithm name...");
                return;
            }

            Stopwatch stopwatch = Stopwatch.StartNew();
            var hashBytes = hMAC.ComputeHash(Encoding.UTF8.GetBytes(text));
            stopwatch.Stop();

            HashResultView(text, stopwatch, hashBytes);
        }

        static void HashView(string text)
        {
            Console.WriteLine("\nUsing hash");

            Console.WriteLine("- SHA1");
            Console.WriteLine("- SHA256");
            Console.WriteLine("- SHA512");
            Console.Write("Algorithm to use: ");
            string hashName = Console.ReadLine();

            HashAlgorithm hashAlgorithm = HashHandler.GetHash(hashName);
            if (hashAlgorithm == null)
            {
                Console.WriteLine("Invalid algorithm name...");
                return;
            }

            Stopwatch stopwatch = Stopwatch.StartNew();
            var hashBytes = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(text));
            stopwatch.Stop();

            HashResultView(text, stopwatch, hashBytes);
        }

        static void HashResultView(string text, Stopwatch stopwatch, byte[] hashBytes)
        {
            Console.Clear();
            Console.WriteLine("Text to hash: " + text);
            Console.WriteLine("Time to compute: " + stopwatch.Elapsed);
            Console.WriteLine("Hash in hex: 0x" + Convert.ToHexString(hashBytes));
            Console.WriteLine("Hash in BASE64: " + Convert.ToBase64String(hashBytes));
           ;
        }
    }
}
