namespace Encrypter
{
    internal static class Encrypter
    {
        static readonly char[] alphabetEncryptionTable = "KLMNOPQRSTUVWXYZABCDEFGHIJ".ToCharArray();

        public static string Encrypt(string text)
        {
            string encryptedText = "";
            string upperText = text.ToUpper();
            for (int i = 0; i < upperText.Length; i++)
            {
                if (upperText[i] == ' ')
                {
                    encryptedText += ' ';
                    continue;
                }

                int index = upperText[i] - 65;
                if (index >= 0 && index < alphabetEncryptionTable.Length)
                {
                    encryptedText += alphabetEncryptionTable[index];
                }
            }
            return encryptedText;
        }

        public static string Decrypt(string text)
        {
            string decryptedText = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ')
                {
                    decryptedText += ' ';
                    continue;
                }

                for (int j = 0; j < alphabetEncryptionTable.Length; j++)
                {
                    if (alphabetEncryptionTable[j] == text[i])
                    {
                        decryptedText += (char)(j + 65);
                    }
                }
            }
            return decryptedText;
        }
    }
}
