using System;
using System.Security.Cryptography;
using System.Text;

namespace lab1
{
    public static class RandomNumbersGenerator
    {
        public const string Symbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        
        private static void Main(string[] args)
        {
            Print(GeneratePseudoRandomNumbers(10), "without seed");
            
            Print(GeneratePseudoRandomNumbers(10, 1234), "with seed 1234");
            Print(GeneratePseudoRandomNumbers(10, 1234), "with seed 1234");

            Print(GeneratePseudoRandomNumbers(10, 1241414), "with seed 1241414");

            Print(GenerateRandomNumber(10));
            Print(GenerateRandomNumber(4));
        }

        public static int[] GeneratePseudoRandomNumbers(int count, int? seed = null)
        {
            Random rnd = seed == null ? new Random() : new Random(seed.Value);
            
            int[] numbers = new int[count];

            for (int i = 0; i < count; i++) 
                numbers[i] = rnd.Next(1, 256);

            return numbers;
        }

        public static byte[] GenerateRandomNumber(int count)
        {
            var randomNumberGenerator =
                new RNGCryptoServiceProvider();

            var randomNumber = new byte[count];
            randomNumberGenerator.GetBytes(randomNumber);
            return randomNumber;
        }

        public static void Print<T>(this T[] elArray, string endMessage = "")
        {
            foreach (T el in elArray)
            {
                Console.Write("{0,5}", $"{el} ");
            }

            Console.WriteLine(endMessage);
        }
        
        public static string GenerateRandomString(int length)
        {
            StringBuilder res = new StringBuilder();
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            
            byte[] uintBuffer = new byte[sizeof(uint)];

            while (length-- > 0)
            {
                rng.GetBytes(uintBuffer);
                uint num = BitConverter.ToUInt32(uintBuffer, 0);
                res.Append(Symbols[(int) (num % (uint) Symbols.Length)]);
            }

            return res.ToString();
        }
        
        public static byte[] GenerateRandomBytes(int length)
        {
            StringBuilder res = new StringBuilder();
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            
            byte[] uintBuffer = new byte[sizeof(uint)];

            while (length-- > 0)
            {
                rng.GetBytes(uintBuffer);
                uint num = BitConverter.ToUInt32(uintBuffer, 0);
                res.Append(Symbols[(int) (num % (uint) Symbols.Length)]);
            }

            return uintBuffer;
        }
    }
}