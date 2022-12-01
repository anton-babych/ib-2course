using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace lab7
{
    public class Eds
    {
        private const string CspContainerName = "RsaContainer";
        
        public byte[] SignMessage(string publicKeyPath, string message)
        {
            byte[] convertedMessage = Encoding.Unicode.GetBytes(message);
            
            var cspParams = new CspParameters
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            using (var rsa = new RSACryptoServiceProvider(2048, cspParams))
            {
                rsa.PersistKeyInCsp = true;
                
                var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
                rsaFormatter.SetHashAlgorithm(nameof(SHA512));
                
                File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
                
                byte[] hashOfData;
                using (var sha512 = SHA512.Create())
                {
                    hashOfData = sha512.ComputeHash(convertedMessage);
                }
                return rsaFormatter.CreateSignature(hashOfData);
            }
        }

        public bool Compare(string publicKeyPath,
            byte[] data,
            byte[] signature)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(publicKeyPath));
                
                var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                rsaDeformatter.SetHashAlgorithm(nameof(SHA512));

                byte[] hashOfData;
                using (var sha512 = SHA512.Create())
                {
                    hashOfData = sha512.ComputeHash(data);
                }
                return rsaDeformatter.VerifySignature(hashOfData, signature);
            } 
        }
    }
}