using System;
using System.Security.Cryptography;
using System.Text;
using lab1;

namespace lab4
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //Task3();
            //Task4();
            Task5();
        }

        private static void Task3()
        {
            new SaltedHash("password").DoSalt();
        }

        private static void Task4() => 
            new PBKDF2(HashAlgorithmName.MD5).With(x => x.HashPassword("password", x.GenerateSalt()));

        private static void Task5()
        {
            var user1 = new User("user1", "user12345");

            user1.TryLoginAs("us", "usfagq");
            user1.TryLoginAs("user1", "user12345");
        }
        
        
        private class User
        {
            private byte[] _password;
            private byte[] _login;
            private byte[] _key = new Byte[64];
            
            public User(string login, string password)
            {
                GenerateKey();
                
                _login = DoHMAC(_key, login);
                _password = DoHMAC(_key, password);
            }

            public bool TryLoginAs(string login, string password)
            {
                var tempLogin = DoHMAC(_key, login);
                var tempPassword = DoHMAC(_key, password);
                
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
            
            private void GenerateKey() => 
                new RNGCryptoServiceProvider().GetBytes(_key);

            private byte[] DoHMAC(byte[] key, string input) => 
                new HMACSHA256(key).ComputeHash(Encoding.UTF8.GetBytes(input));
        }
    }
}