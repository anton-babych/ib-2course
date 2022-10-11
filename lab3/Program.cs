using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using lab1;
using lab2;

namespace lab3
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            
            Task2();
            Task3();
            Task4();
            
        }

        private static void Task2()
        {
            Test test = new Test("0123456789".ToCharArray(),
                8,
                8,
                Guid.Parse("564c8da6-0440-88ec-d453-0bbad57c6036"));

            Test.Do();
        }

        private static void Task3()
        {
            //byte[] secretkey = new Byte[64];
            //new RNGCryptoServiceProvider().GetBytes(secretkey);

            byte[] secretkey = Encoding.UTF8.GetBytes(RandomNumbersGenerator.GenerateRandomString(10));

            string inputMessage = "kakoe to message";

            SignMessage(secretkey, inputMessage, out var signedMessage);
            
            VerifyMassage(secretkey, inputMessage, signedMessage);
        }

        public static void SignMessage(byte[] key, string message, out byte[] s) => 
            s = new HMACSHA256(key).ComputeHash(Encoding.UTF8.GetBytes(message));

        public static void VerifyMassage(byte[] key, string message, byte[] signed)
        {
            bool err = false;
            byte[] computedHash = new HMACSHA256(key).ComputeHash(Encoding.UTF8.GetBytes(message));

            for (int i = 0; i < signed.Length; i++)
            {
                Console.WriteLine($"computer {computedHash[i]} signed {signed[i]}");
                if (computedHash[i] != signed[i])
                {
                    err = true;
                }
            }

            if (err)
            {
                Console.WriteLine("diff");
            }
            else
            {
                Console.WriteLine("same");
            }
        }

        private static void Task4()
        {
            var user1 = new User("anton", "12345");
            user1.TryLogin("anton", "12345");
            
            var user2 = new User("chel", "15151");
            user2.TryLogin("necafqa", "15151");
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

            public bool TryLogin(string login, string password)
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