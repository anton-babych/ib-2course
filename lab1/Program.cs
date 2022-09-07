using System;
using System.Security.Cryptography;

namespace lab1
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Debug(GeneratePseudoRandomNumbers(10, 1234));
            Debug(GeneratePseudoRandomNumbers(10, 1234));
            Debug(GeneratePseudoRandomNumbers(10, 1241414));
            Debug(GeneratePseudoRandomNumbers(10, null));

            Debug(GenerateRandomNumber(10));
            Debug(GenerateRandomNumber(10));
        }

        private static int[] GeneratePseudoRandomNumbers(int count, int? seed)
        {
            int[] numbers = new int[count];

            Random rnd = seed == null ? new Random() : new Random(seed.Value);

            for (int i = 0; i < count; i++)
            {
                var num = rnd.Next(-10, 11);
                numbers[i] = num;
            }

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

        private static void Debug(int[] numbs)
        {
            foreach (int num in numbs)
            {
                Console.Write("{0,5}", $"{num} ");
            }

            Console.WriteLine();
        }

        private static void Debug(byte[] numbs)
        {
            foreach (byte num in numbs)
            {
                Console.Write("{0,5}", $"{num} ");
            }

            Console.WriteLine();

        }

    }
}