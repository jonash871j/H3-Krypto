using ConsoleEngineCS.Core;
using SymmetricEncryption.Encryption;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SymmetricEncryption.Views
{
    internal class EncryptionView : View
    {
        private int menuIndex = 0;
        private int comboIndex = 0;
        private SymmetricAlgorithm symmetricAlgorithm = null;

        public EncryptionView(ViewHandler viewHandler) : base(viewHandler)
        {
        }

        public string EncryptionKey { get; private set; } = ".";
        public string IV { get; private set; } = ".";
        public string Text { get; private set; } = "";
        public List<string> EncryptionOptions = new List<string>() { "DES", "TripleDES" };

        public override void Initialize()
        {
            ConsoleEx.Create(80, 20);
            ConsoleEx.SetFont("Consolas", 16, 32);
        }

        public override void Logic()
        {
            if (Input.KeyPressed(Key.Up))
            {
                menuIndex--;
            }
            if (Input.KeyPressed(Key.Down))
            {
                menuIndex++;
            }
            if (menuIndex < 0)
            {
                menuIndex = 1;
            }
            if (menuIndex > 1)
            {
                menuIndex = 0;
            }

            if (Input.KeyPressed(Key.Left) && menuIndex == 0)
            {
                comboIndex--;
            }
            if (Input.KeyPressed(Key.Right) && menuIndex == 0)
            {
                comboIndex++;
            }
            if (comboIndex < 0)
            {
                comboIndex = EncryptionOptions.Count -1;
            }
            if (comboIndex > EncryptionOptions.Count - 1)
            {
                comboIndex = 0;
            }
            if (Input.KeyPressed(Key.Left) ||Input.KeyPressed(Key.Right) ||symmetricAlgorithm == null)
            {
                symmetricAlgorithm = EncryptionHandler.GetSymmetricAlgorithm(EncryptionOptions[comboIndex]);
            }

            Text = ReadInput(1, Text);
        }

        public string ReadInput(int menuIndex, string value)
        {
            return menuIndex == this.menuIndex ? Input.Read(value) : value;
        }

        public override void Drawing()
        {
            string title = "\ffSymmetric Encryption\n";
            ConsoleEx.SetPosition(title.Length / 2 + ConsoleEx.Width / 4, 0);
            ConsoleEx.WriteLine(title);

            ConsoleEx.SetPosition(2, 2);
            ConsoleEx.WriteLine("\f9Encryption options");
            ComboField(0, EncryptionOptions);
            ConsoleEx.WriteLine(" Key: 0x" + Convert.ToHexString(symmetricAlgorithm?.Key));
            ConsoleEx.WriteLine("  IV: 0x" + Convert.ToHexString(symmetricAlgorithm?.IV));

            ConsoleEx.WriteLine("\n\f9Plain text");
            InputField(1, "ASCII: " + Text);
            ConsoleEx.WriteLine("\f8-   HEX: 0x" + Convert.ToHexString(Encoding.ASCII.GetBytes(Text)));

            ConsoleEx.WriteLine("\n\f9Chipher text");
            ConsoleEx.WriteLine("\f8- ASCII: 0x");
            ConsoleEx.WriteLine("\f8-   HEX: 0x");

            ConsoleEx.SetPosition(0, ConsoleEx.Height - 1);
            ConsoleEx.Write("\feARROW KEYS: \ffMenu movement    \feCTRL: \ffAuto generate key and IV");

            ConsoleEx.Update();
            ConsoleEx.Clear();
        }

        private void InputField(int menuIndex, string text)
        {
            if (menuIndex == this.menuIndex)
            {
                ConsoleEx.WriteLine("\fa* \ff" + text + "|");
            }
            else
            {
                ConsoleEx.WriteLine("- \ff" + text);
            }
        }

        public void ComboField(int menuIndex, List<string> options)
        {
            if (menuIndex == this.menuIndex)
            {
                ConsoleEx.Write("\fa* \ff");
            }
            else
            {
                ConsoleEx.Write("- \ff");
            }

            for (int i = 0; i < options.Count; i++)
            {
                if (i == comboIndex)
                {
                    ConsoleEx.Write($"\fc{options[i]}\ff, ");
                }
                else
                {
                    ConsoleEx.Write(options[i] + ", ");
                }
            }
            ConsoleEx.WriteLine();
        }
    }
}
