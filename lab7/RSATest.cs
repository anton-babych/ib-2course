using System.IO;
using System.Security.Cryptography;

namespace lab7
{
    public class RsaTest
    {
        private static RSAParameters _publicKey, _privateKey;
        private static readonly string CspContainerName = "RsaContainer";
        
        public void AssignNewKey()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                
                _publicKey = rsa.ExportParameters(false);
                _privateKey = rsa.ExportParameters(true);
            }
        }
        
        public void AssignNewKey(string publicKeyPath)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                
                File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
                _privateKey = rsa.ExportParameters(true);
            }
        }

        public void AssignNewKey(string publicKeyPath, string privateKeyPath)
        {
            CspParameters cspParameters = new CspParameters(1)
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore,
                ProviderName = "Microsoft Strong Cryptographic Provider"
            };
            var rsa = new RSACryptoServiceProvider(cspParameters)
            {
                PersistKeyInCsp = true
            };
            
            File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
        }

        public byte[] EncryptFromMemory(byte[] data) => 
            Encrypt(data, _publicKey);

        public byte[] DecryptFromMemory(byte[] data) => 
            Decrypt(data, _privateKey);

        public byte[] EncryptFromFile(string publicKeyPath, byte[] dataToEncrypt) =>
            Encrypt(publicKeyPath, dataToEncrypt);

        public byte[] DecryptFromFile(string chipherTextPath)
        {
            byte[] data = File.ReadAllBytes(chipherTextPath);
            
            return Decrypt(data, _privateKey);
        }

        public byte[] EncryptFromContainer(string publicKeyPath, byte[] dataToEncrypt)
        {
            var cspParams = new CspParameters
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore
            };

            return Encrypt(dataToEncrypt, publicKeyPath, cspParams);
        }

        public byte[] DecryptFromContainer(byte[] dataToDecrypt)
        {
            var cspParams = new CspParameters
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            
            return Decrypt(dataToDecrypt, cspParams);
        }

        private byte[] Encrypt(byte[] dataToEncrypt, RSAParameters publicKey)
        {
            byte[] cipherBytes;
            
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.ImportParameters(publicKey);
                cipherBytes = rsa.Encrypt(dataToEncrypt, true);
            }

            return cipherBytes;
        }

        private byte[] Encrypt(string publicKeyPath, byte[] 
            dataToEncrypt)
        {
            byte[] chipherBytes;
           
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(publicKeyPath));
                chipherBytes = rsa.Encrypt(dataToEncrypt, true);
            }

            return chipherBytes;
        }

        private byte[] Encrypt(byte[] dataToEncrypt, string publicKey, CspParameters cspParameters)
        {
            byte[] cipherBytes;

            using (var rsa = new RSACryptoServiceProvider(cspParameters))
            {
                rsa.PersistKeyInCsp = true;
                cipherBytes = rsa.Encrypt(dataToEncrypt, true);
            }

            return cipherBytes;
        }

        private byte[] Decrypt(byte[] encryptedBytes, RSAParameters privateKey)
        {
            byte[] plainTextBytes;

            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(privateKey);
                plainTextBytes = rsa.Decrypt(encryptedBytes, true);
            }
            return plainTextBytes;
        }

        private static byte[] Decrypt(byte[] dataToDecrypt, CspParameters cspParams)
        {
            byte[] plainBytes;
            using (var rsa = new RSACryptoServiceProvider(cspParams))
            {
                rsa.PersistKeyInCsp = true;
                plainBytes = rsa.Decrypt(dataToDecrypt, true);
            }

            return plainBytes;
        }
    }
}