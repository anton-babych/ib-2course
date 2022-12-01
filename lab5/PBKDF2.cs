using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;
using lab3;
using lab4;

namespace lab4
{
    public class PBKDF2
    {
        private readonly HashAlgorithmName _algorithm;

        public PBKDF2(HashAlgorithmName algorithm)
        {
            _algorithm = algorithm;
        }

        public byte[] GenerateSalt()
        {
            var randomNumberGenerator =
                new RNGCryptoServiceProvider();
            var randomNumber = new byte[32];
            randomNumberGenerator.GetBytes(randomNumber);
            return randomNumber;
        }

        public static byte[] HashPassword(byte[] toBeHashed,
            byte[] salt, Int32 numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds))
            {
                return rfc2898.GetBytes(20);
            }
        }

        [SuppressMessage("ReSharper.DPA", "DPA0002: Excessive memory allocations in SOH", MessageId = "type: System.Byte[]")]
        public byte[] HashPasswordWithTimes(string passwordToHash, byte[] salt, int times = 10, int interactionCount = 10000, int step = 50000)
        {
            byte[] startString = Encoding.UTF8.GetBytes(passwordToHash);
            byte[] hashedPassword = startString;
           
            for (int i = 0; i < times; i++, interactionCount += step)
            {
                var sw = new Stopwatch();
                sw.Start();
                
                hashedPassword = HashPassword(hashedPassword,
                    salt, interactionCount);
                
                sw.Stop();
                Console.WriteLine("Iterations: " + interactionCount + " Elapsed Time : " + sw.ElapsedMilliseconds + "ms");
            }

            return hashedPassword;
        }
    }
}