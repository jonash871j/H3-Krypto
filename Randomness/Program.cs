using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;

namespace Randomness
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestSystemRandom();
            TestRNGCryptoServiceProvider();
        }

        static void TestRNGCryptoServiceProvider()
        {
            Stopwatch stopwatch = new Stopwatch();
            byte[] randomBytes = new byte[1024];
            int[] countArray = new int[256];
            int[] finalCountArray = new int[1024];
            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
            stopwatch.Start();
            random.GetBytes(randomBytes);

            stopwatch.Stop();
            for (int i = 0; i < randomBytes.Length; i++)
            {
                countArray[randomBytes[i]]++;
            }
            for (int i = 0; i < countArray.Length; i++)
            {
                finalCountArray[countArray[i]]++;
            }
            Console.WriteLine("System.Security.Cryptography.RNGCryptoServiceProvider");
            Console.WriteLine("Random time: " + stopwatch.Elapsed);
            for (int i = 0; i < finalCountArray.Length; i++)
            {
                if (finalCountArray[i] == 0)
                {
                    continue;
                }
                Console.WriteLine($"How many times a number has been take {i} time is {finalCountArray[i]}");
            }
        }

        static void TestSystemRandom()
        {
            Stopwatch stopwatch = new Stopwatch();
            int[] randomIntegers = new int[1024];
            int[] countArray = new int[256];
            int[] finalCountArray = new int[1024];
            Random random = new Random();
            stopwatch.Start();
            for (int i = 0; i < randomIntegers.Length; i++)
            {
                randomIntegers[i] = random.Next(0, 256);
            }
            stopwatch.Stop();
            for (int i = 0; i < randomIntegers.Length; i++)
            {
                countArray[randomIntegers[i]]++;
            }
            for (int i = 0; i < countArray.Length; i++)
            {
                finalCountArray[countArray[i]]++;
            }
            Console.WriteLine("System.Random");
            Console.WriteLine("Random time: " + stopwatch.Elapsed);
            for (int i = 0; i < finalCountArray.Length; i++)
            {
                if (finalCountArray[i] == 0)
                {
                    continue;
                }
                Console.WriteLine($"How many times a number has been take {i} time is {finalCountArray[i]}");
            }
        }
    }
}
