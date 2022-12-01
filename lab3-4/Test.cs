using System;
using System.Diagnostics;
using System.Linq;

namespace lab3
{
    public class Test
    {
        public static string Result;

        public Test(char[] allCharacters, int from, int to, Guid guidToFind)
        {
            _charactersToTest = allCharacters;
            _from = @from;
            _to = to;
            _guidToFind = guidToFind;
        }

        private static char[] _charactersToTest;
        private readonly int _from;
        private readonly int _to;

        private static bool _isMatched = false;

        private static int _charactersToTestLength = 0;
        private static long _computedKeys = 0;

        private static Guid _guidToFind;

        public static void Do()
        {
            var timeStarted = DateTime.Now;
            Console.WriteLine("Start BruteForce - {0}", timeStarted.ToString());

            _charactersToTestLength = _charactersToTest.Length;

            var estimatedPasswordLength = 8;

            StartBruteForce(estimatedPasswordLength);

            Console.WriteLine("Password matched. - {0}", DateTime.Now.ToString());
            Console.WriteLine("Time passed: {0}s", DateTime.Now.Subtract(timeStarted).TotalSeconds);
            Console.WriteLine("Resolved password: {0}", Result);
            Console.WriteLine("Computed keys: {0}", _computedKeys);

            Console.ReadLine();
        }

        private static void StartBruteForce(int keyLength)
        {
            var keyChars = CreateCharArray(keyLength, _charactersToTest[0]);
            var indexOfLastChar = keyLength - 1;
            CreateNewKey(0, keyChars, keyLength, indexOfLastChar);
        }

        private static char[] CreateCharArray(int length, char defaultChar) =>
            (from c in new char[length] select defaultChar).ToArray();

        private static void CreateNewKey(int currentCharPosition, char[] keyChars, int keyLength, int indexOfLastChar)
        {
            var nextCharPosition = currentCharPosition + 1;

            for (int i = 0; i < _charactersToTestLength; i++)
            {
                keyChars[currentCharPosition] = _charactersToTest[i];

                if (currentCharPosition < indexOfLastChar)
                    CreateNewKey(nextCharPosition, keyChars, keyLength, indexOfLastChar);
                else
                {
                    _computedKeys++;

                    string str = new String(keyChars);
                    var myGuid = Hashing.ComputeGuid(Hashing.ComputeHashMd5(str));

                    //Console.WriteLine(str);
                    
                    if (myGuid != _guidToFind) continue;

                    if (_isMatched) return;

                    _isMatched = true;
                    Result = str;
                    return;
                }
            }
        }
    }
}