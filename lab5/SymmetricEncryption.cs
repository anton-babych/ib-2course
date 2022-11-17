using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace lab6
{
    public class SymmetricEncryption
    {
        private readonly CipherName _name;

        public enum CipherName
        {
            Des,
            TripleDes,
            Aes
        }

        public SymmetricEncryption(CipherName name)
        {
            _name = name;
        }

        public byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            SymmetricAlgorithm cr = GetCurrentAlgorithm();

            using (cr)
            {
                cr.Mode = CipherMode.CBC;
                cr.Padding = PaddingMode.PKCS7;
                cr.Key = key;
                cr.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream =
                        new CryptoStream(memoryStream, cr.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }

        }

        public byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
        {
            SymmetricAlgorithm cr = GetCurrentAlgorithm();
            
            using (cr)
            {
                cr.Mode = CipherMode.CBC;
                cr.Padding = PaddingMode.PKCS7;
                cr.Key = key;
                cr.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream =
                        new CryptoStream(memoryStream, cr.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }

        private SymmetricAlgorithm GetCurrentAlgorithm()
        {
            switch (_name)
            {
                case CipherName.Des:
                    return new DESCryptoServiceProvider();
                case CipherName.TripleDes:
                    return new TripleDESCryptoServiceProvider();
                case CipherName.Aes:
                    return new AesCryptoServiceProvider();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}