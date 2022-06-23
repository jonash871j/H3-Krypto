using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace AsymmetricEncryptionWPF
{
    public partial class MainWindow : Window
    {
        private RSACryptoServiceProvider rSACryptoServiceProvider = new(4096);

        public MainWindow() 
            => InitializeComponent();

        private void Window_Loaded(object sender, RoutedEventArgs e)
            => UpdateTextboxes();

        private void MI_Exit_Click(object sender, RoutedEventArgs e)
            => Close();

        private void MI_GenerateNewKeys_Click(object sender, RoutedEventArgs e)
        {
            rSACryptoServiceProvider = new RSACryptoServiceProvider(4096);
            UpdateTextboxes();
        }

        private void BN_Decrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                byte[] chipherBytes = Convert.FromHexString(TB_PrivateChipherBytes.Text.Replace("-", ""));
                TB_PrivatePlainText.Text = Encoding.ASCII.GetString(rSACryptoServiceProvider.Decrypt(chipherBytes, true));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to decrypt: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BN_Encrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Exports public key into byte array
                byte[] publicKeyData = rSACryptoServiceProvider.ExportRSAPublicKey();

                // Generates a rsaCryptoServiceProvider using the public key there only can encrypt
                RSACryptoServiceProvider publicRSACryptoServiceProvider = new();
                publicRSACryptoServiceProvider.ImportRSAPublicKey(publicKeyData, out _);

                byte[] plainTextBytes = Encoding.ASCII.GetBytes(TB_PublicPlainText.Text);
                TB_PublicChipherBytes.Text = BitConverter.ToString(publicRSACryptoServiceProvider.Encrypt(plainTextBytes, true));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to encrypt: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateTextboxes()
        {
            RSAParameters rSAParameters = rSACryptoServiceProvider.ExportParameters(true);
            TB_PublicExponent.Text = BitConverter.ToString(rSAParameters.Exponent ?? Array.Empty<byte>());
            TB_PublicModulus.Text = BitConverter.ToString(rSAParameters.Modulus ?? Array.Empty<byte>());
            TB_PrivateExponent.Text = BitConverter.ToString(rSAParameters.Exponent ?? Array.Empty<byte>());
            TB_PrivateModulus.Text = BitConverter.ToString(rSAParameters.Modulus ?? Array.Empty<byte>());
            TB_PrivateD.Text = BitConverter.ToString(rSAParameters.D ?? Array.Empty<byte>());
            TB_PrivateDP.Text = BitConverter.ToString(rSAParameters.DP ?? Array.Empty<byte>());
            TB_PrivateDQ.Text = BitConverter.ToString(rSAParameters.DQ ?? Array.Empty<byte>());
            TB_PrivateInverseQ.Text = BitConverter.ToString(rSAParameters.InverseQ ?? Array.Empty<byte>());
            TB_PrivateP.Text = BitConverter.ToString(rSAParameters.P ?? Array.Empty<byte>());
            TB_PrivateQ.Text = BitConverter.ToString(rSAParameters.Q ?? Array.Empty<byte>());
        }
    }
}
