using System;
using System.Security.Cryptography;
using System.Text;

namespace lab4
{
    public class User
    {
        private readonly byte[] _password;
        private readonly byte[] _login;
        private readonly byte[] _salt;
            
        public User(string login, string password, int iterationCount)
        {
            _salt = GenerateRandomSalt();
                
            _login = GetStringInBytes(login);
            _password = GenerateSalt(password);
        }

        private byte[] GenerateRandomSalt()
        {
            return new PBKDF2(HashAlgorithmName.MD5).GenerateSalt();
        }

        private byte[] GenerateSalt(string password)
        {
            PBKDF2 x = new PBKDF2(HashAlgorithmName.MD5);
            return x.HashPassword(password, _salt);
        }

        public bool TryLogin(string login, string password)
        {
            var tempLogin = GetStringInBytes(login);
            var tempPassword = GenerateSalt(password);

            for (int i = 0; i < tempLogin.Length; i++)
            {
                if (tempLogin[i] != _login[i])
                {
                    Console.WriteLine($"fail to login: login {login} is incorrect");
                    return false;
                }
            }
                
            for (int i = 0; i < tempPassword.Length; i++)
            {
                if (tempPassword[i] != _password[i])
                {
                    Console.WriteLine($"fail to login: password {password} is incorrect");
                    return false;
                }
            }

            Console.WriteLine("login succeed");
            return true;
        }

        private static byte[] GetStringInBytes(string login) => 
            Encoding.Unicode.GetBytes(login);
    }
}