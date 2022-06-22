using SymmetricEncryption.Views;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SymmetricEncryption
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Key: 0x");
            //Console.WriteLine("IV: 0x");

            Console.WrtieLine("Text: " + )

            byte[] buffer = Encoding.UTF8.GetBytes("Hello World");
            var a = TripleDES.Create();
            a.GenerateKey();
            a.GenerateIV();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, a.CreateEncryptor(a.Key, a.IV), CryptoStreamMode.Write);
            cs.Write(buffer);
            cs.Close();
            Console.WriteLine(Encoding.UTF8.GetString(ms.ToArray()));
            //ViewHandler viewHandler = new();
            //viewHandler.ChangeView(ViewKey.Input);

            //while (true)
            //{
            //    View view = viewHandler.GetCurrentView();
            //    view.Logic();
            //    view.Drawing();
            //}
        }
    }
}
