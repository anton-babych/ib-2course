using System;
using System.Security.Cryptography;

namespace lab1
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            PrintArray(GeneratePseudoRandomNumbers(10), "without seed");
            
            PrintArray(GeneratePseudoRandomNumbers(10, 1234), "with seed 1234");
            PrintArray(GeneratePseudoRandomNumbers(10, 1234), "with seed 1234");

            PrintArray(GeneratePseudoRandomNumbers(10, 1241414), "with seed 1241414");

            PrintArray(GenerateRandomNumber(10));
            PrintArray(GenerateRandomNumber(10));
        }

        private static int[] GeneratePseudoRandomNumbers(int count, int? seed = null)
        {
            Random rnd = seed == null ? new Random() : new Random(seed.Value);
            
            int[] numbers = new int[count];

            for (int i = 0; i < count; i++) 
                numbers[i] = rnd.Next(1, 256);

            return numbers;
        }

        private static byte[] GenerateRandomNumber(int count)
        {
            var randomNumberGenerator =
                new RNGCryptoServiceProvider();

            var randomNumber = new byte[count];
            randomNumberGenerator.GetBytes(randomNumber);
            return randomNumber;
        }

        private static void PrintArray<T>(T[] elArray, string endMessage = "")
        {
            foreach (T el in elArray)
            {
                Console.Write("{0,5}", $"{el} ");
            }

            Console.WriteLine(endMessage);
        }
    }
}