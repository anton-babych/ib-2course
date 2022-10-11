using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using lab1;

namespace lab2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //string openText = File.ReadAllText("file.dat");
            string openText = "stol214151";
            Console.WriteLine("open text: " + openText);

            string key = RandomNumbersGenerator.GenerateRandomString(openText.Length);
            Console.WriteLine("key: " + key);

            var cipherText = Encrypt(openText, key);
            Console.WriteLine("cipher: " + cipherText);

            var decodedText = Decrypt(cipherText, key);
            Console.WriteLine("open text: " + decodedText);

            var cipherPhraseText = Encrypt("aaMit21c", key);
            Console.WriteLine("ciphermit21: " + cipherPhraseText);
            TryBruteForce(@"?g	W2f	W9,FFTH8b7K7p$[KUee@W,^IW,\W9,FEEe G1", out string decodedPhraseText);
            
            //GetCombinations(2);
        }

        public static List<string> GetCombinations(string allSymbols, int length)
        {
            var combinations = CombAndPerm.GetCombinationsAndPerm(allSymbols, length);
            List<string> st = new List<string>();

            foreach (var comb in combinations)
            {
                CombAndPerm.GetPer(comb, out List<string> pers);
                foreach (var per in pers)
                {
                    Console.WriteLine(per);
                    st.Add(per);
                }
            }

            return st;
        }

        public static string Encrypt(string openText, string key)
        {
            var length = openText.Length;

            string cipherText = "";

            for (int i = 0; i < length; i++)
                cipherText += (char) (openText[i] ^ key[i]);

            return cipherText;
        }

        public static string Decrypt(string cipherText, string key)
        {
            var length = key.Length;
            string openText = "";

            for (int i = 0; i < length; i++)
                openText += (char) (cipherText[i] ^ key[i]);

            return openText;
        }

        public static bool TryBruteForce(string cipherText, out string decodedText, string phrase = "Mit21",
            int length = 7)
        {
            string symbols = RandomNumbersGenerator.Symbols;
            char[] symbolsArray = symbols.ToCharArray();
            
            int phraseLength = phrase.Length;
            int possibleTries = length - phraseLength + 1;

            decodedText = "";
            string tempKey = "";

            List<string> extraCombinations = GetCombinations(RandomNumbersGenerator.Symbols,length - phraseLength);
            
            for (int blocksToShiftPhrase = 0; blocksToShiftPhrase < possibleTries; blocksToShiftPhrase++)
            {
                var withOnlyPhrase = GenerateWithOnlyPhrase(phrase, length, phraseLength, blocksToShiftPhrase);

                for (int j = 0; j < length; j++)
                {
                    if (withOnlyPhrase[j] != '\0') continue;

                    /*foreach (var combination in extraCombinations)
                    {
                        char[] tempArrayWithCombinations = new char[withOnlyPhrase.Length];
                        Array.Copy(withOnlyPhrase, tempArrayWithCombinations, withOnlyPhrase.Length);

                        tempArrayWithCombinations[j] = combination; 
                        
                        
                        char[] m = new char[length];
                        for (int i = 0; i < length; i++) 
                            m[i] = (char) (tempArrayWithCombinations[i] ^ cipherText[i]);

                       
                        
                        if (new string(tempArrayWithCombinations) == cipherText)
                        {
                            Console.WriteLine("---------done---------");
                            return true;
                        }
                    }*/
                }
            }

            Console.WriteLine("not found");
            return false;
        }

        private static char[] GenerateWithOnlyPhrase(string phrase, int length, int phraseLength, int blocksToShiftPhrase)
        {
            char[] tempArrayWithPhrase = new char[length];
            for (int i = 0; i < phraseLength; i++)
            {
                tempArrayWithPhrase[blocksToShiftPhrase + i] = phrase[i];
            }

            return tempArrayWithPhrase;
        }
    }
}