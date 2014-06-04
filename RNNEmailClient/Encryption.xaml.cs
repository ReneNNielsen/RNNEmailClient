using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Security.Cryptography;
using System.IO;

namespace RNNEmailClient
{
    /// <summary>
    /// Interaction logic for Encryption.xaml
    /// </summary>
    public partial class Encryption : Window
    {
        public Encryption()
        {
            InitializeComponent();
        }
        byte[] encrypted;
        byte[] encryptionKey;
        byte[] encryptionIV;
        /// <summary>
        /// Encrypt the text in txtText when click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void btnEncryption_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (RijndaelManaged myRijndael = new RijndaelManaged())
                {
                    myRijndael.GenerateKey();
                    myRijndael.GenerateIV();
                    encryptionKey = myRijndael.Key;
                    encryptionIV = myRijndael.IV;
                    encrypted = EncryptStringToBytes(txtText.Text, encryptionKey, encryptionIV);
                    BlockText.Text = "";
                    foreach (byte item in encrypted)
                    {
                        BlockText.Text += item.ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: {0}", ex.Message);
            }
        }
        static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encryptedData;
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encryptedData = msEncrypt.ToArray();
                    }
                }
            }
            return encryptedData;
        }
        private void btnDecryption_Click(object sender, RoutedEventArgs e)
        {
            if (encrypted == null || encrypted.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (encryptionKey == null || encryptionKey.Length <= 0)
                throw new ArgumentNullException("Key");
            if (encryptionIV == null || encryptionIV.Length <= 0)
                throw new ArgumentNullException("IV");
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = encryptionKey;
                rijAlg.IV = encryptionIV;

                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(encrypted))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            BlockText.Text = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

        }
    }
}
