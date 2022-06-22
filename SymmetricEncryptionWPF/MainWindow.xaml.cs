using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SymmetricEncryptionWPF
{
    public partial class MainWindow : Window
    {
        private SymmetricAlgorithm symmetricAlgorithm;

        public MainWindow()
        {
            symmetricAlgorithm = EncryptionHandler.GetSymmetricAlgorithm("DES");
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateResultTextboxes();
        }

        private void BN_GenerateKeyAndIV_Click(object sender, RoutedEventArgs e)
        {
            symmetricAlgorithm.GenerateKey();
            symmetricAlgorithm.GenerateIV();
            UpdateResultTextboxes();
        }

        private void BN_Encrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                symmetricAlgorithm.Key = GetBase64OrAsciiBytes(TB_Key.Text);
                symmetricAlgorithm.IV = GetBase64OrAsciiBytes(TB_IV.Text);

                Stopwatch stopwatch = Stopwatch.StartNew();
                byte[] chipherBytes = Encrypter.Encrypt(symmetricAlgorithm, TB_PlainTextASCIIOrBase64.Text);
                stopwatch.Stop();

                TB_ChipherTextASCIIOrBase64.Text = Convert.ToBase64String(chipherBytes);
                TB_ChipherTextHex.Text = "0x" + Convert.ToHexString(chipherBytes);
                LB_Time.Content = "Time: " + stopwatch.Elapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to encrypt: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BN_Decrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                symmetricAlgorithm.Key = GetBase64OrAsciiBytes(TB_Key.Text);
                symmetricAlgorithm.IV = GetBase64OrAsciiBytes(TB_IV.Text);
                
                Stopwatch stopwatch = Stopwatch.StartNew();
                string plainText = Encrypter.Decrypt(symmetricAlgorithm, GetBase64OrAsciiBytes(TB_PlainTextASCIIOrBase64.Text));
                stopwatch.Stop();

                TB_ChipherTextASCIIOrBase64.Text = plainText;
                TB_ChipherTextHex.Text = "0x" + Convert.ToHexString(GetBase64OrAsciiBytes(plainText));
                LB_Time.Content = "Time: " + stopwatch.Elapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to decrypt: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CoB_Algorithm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string option = (e.AddedItems[0] as ComboBoxItem)?.Content as string ?? "";
            symmetricAlgorithm = EncryptionHandler.GetSymmetricAlgorithm(option);
            UpdateResultTextboxes();
        }

        private void TB_PlainTextASCII_TextChanged(object sender, TextChangedEventArgs e)
        {
            TB_PlainTextHex.Text = "0x" + Convert.ToHexString(GetBase64OrAsciiBytes(TB_PlainTextASCIIOrBase64.Text));
        }

        private void UpdateResultTextboxes()
        {
            if (!IsLoaded)
            {
                return;
            }

            TB_Key.Text = Convert.ToBase64String(symmetricAlgorithm.Key);
            TB_IV.Text = Convert.ToBase64String(symmetricAlgorithm.IV);
        }

        private static byte[] GetBase64OrAsciiBytes(string str)
        {
            try
            {
                return Convert.FromBase64String(str);
            }
            catch
            {
                return Encoding.ASCII.GetBytes(str);
            }
        }
    }
}
