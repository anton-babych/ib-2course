using System;
using System.Security.Cryptography;
using System.Text;
using lab1;

namespace lab4
{
    public class SaltedHash
    {
        private readonly byte[] _result;

        public SaltedHash(string password)
        {
            var text = Encoding.UTF8.GetBytes(password);
            Console.WriteLine("pasword is: " + password);
            
            var salt = GenerateSalt();
            Console.WriteLine("salt is: " + Convert.ToBase64String(salt));

            _result = HashPasswordWithSalt(text, salt);
            Console.WriteLine("result is: " + Convert.ToBase64String(_result));
        }

        public byte[] GetResult() => _result;

        private byte[] HashPasswordWithSalt(byte[] toBeHashed, byte[] salt) => 
            SHA256.Create().ComputeHash(Combine(toBeHashed, salt));

        private byte[] Combine(byte[] passw, byte[] salt)
        {
            var ret = new byte[passw.Length + salt.Length]; 
            Buffer.BlockCopy(passw, 0, ret, 0, passw.Length); 
            Buffer.BlockCopy(salt, 0, ret, passw.Length, salt.Length);
            return ret;
        }

        private byte[] GenerateSalt() => 
            RandomNumbersGenerator.GenerateRandomBytes(256);
    }
}