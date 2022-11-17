using System;
using System.Security.Cryptography;
using System.Text;
using lab1;

namespace lab6
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Ex1();
        }

        private static void Ex1()
        {
            Demonstrate("testing aes", SymmetricEncryption.CipherName.Aes, 32, 16);
            Demonstrate("testing des", SymmetricEncryption.CipherName.Des, 8,8);
            Demonstrate("testing triple des", SymmetricEncryption.CipherName.TripleDes, 16, 8);
        }

        private static void Demonstrate(string testString, SymmetricEncryption.CipherName name, int keyLength, int ivLength)
        {
            var encryption = new SymmetricEncryption(name);
            
            var key = RandomNumbersGenerator.GenerateRandomNumber(keyLength);
            var iv = RandomNumbersGenerator.GenerateRandomNumber(ivLength);
            
            var encrypted = encryption.Encrypt(Encoding.UTF8.GetBytes(testString), key, iv);
            var decrypted = encryption.Decrypt(encrypted, key, iv);
            
            var decryptedMessage = Encoding.UTF8.GetString(decrypted);
            
            Console.WriteLine($"\n{name} Demonstration");
            Console.WriteLine($"Original Text: {testString}");
            Console.WriteLine($"Encrypted Text: {Convert.ToBase64String(encrypted)}");
            Console.WriteLine($"Decrypted Text: {decryptedMessage}");
        }
    }
}