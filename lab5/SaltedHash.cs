using System;
using System.Security.Cryptography;
using System.Text;
using lab1;

namespace lab4
{
    public class SaltedHash
    {
        private readonly string _password;
        
        private byte[] _result;

        public SaltedHash(string password)
        {
            _password = password;
        }

        public SaltedHash()
        {
            
        }
        public byte[] DoSalt()
        {
            var text = Encoding.UTF8.GetBytes(_password);
            Console.WriteLine("pasword is: " + _password);
            
            var salt = GenerateSalt();
            Console.WriteLine("salt is: " + Convert.ToBase64String(salt));

            _result = HashPasswordWithSalt(text, salt);
            Console.WriteLine("result is: " + Convert.ToBase64String(_result));

            return _result;
        }

        private byte[] HashPasswordWithSalt(byte[] toBeHashed, byte[] salt) => 
            SHA256.Create().ComputeHash(Combine(toBeHashed, salt));

        private byte[] Combine(byte[] passw, byte[] salt)
        {
            var ret = new byte[passw.Length + salt.Length]; 
            Buffer.BlockCopy(passw, 0, ret, 0, passw.Length); 
            Buffer.BlockCopy(salt, 0, ret, passw.Length, salt.Length);
            return ret;
        }

        public byte[] GenerateSalt() => 
            RandomNumbersGenerator.GenerateRandomBytes(32);
    }
}