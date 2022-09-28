using System;
using System.Collections.Generic;
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
            /*byte[] computeHashMd5 = ComputeHashMd5("01234567");
            string format = Convert.ToBase64String(computeHashMd5);
            Console.WriteLine("6Ts6DTbarv+YS+RM8JbiNg==" == format);
            //byte[] bytes = Encoding.Unicode.GetBytes(format);
            Console.WriteLine(format);
            Console.WriteLine(ComputeGuid(computeHashMd5));*/

            var combs2 = Extension.GetCombinationsAndPerm("0123456789", 8, false);
            
            if(TryFindPasswordFromCombiantions(combs2, out var password)) 
                Console.WriteLine($"Found: password is {password}");
        }

        private static bool TryFindPasswordFromCombiantions(List<string> combs, out string password)
        {
            Guid guidToFind = Guid.Parse("564c8da6-0440-88ec-d453-0bbad57c6036");
            
            password = "";
            foreach (var comb in combs)
            {
                //int.TryParse(comb, out var num);
                //var myGuid = ComputeGuid(ComputeHashMd5(BitConverter.GetBytes(num)));
                
                var myGuid = ComputeGuid(ComputeHashMd5(comb));
                
                Console.WriteLine($"{myGuid} {myGuid == guidToFind} {guidToFind}");
                
                if (myGuid != guidToFind) continue;
                password = comb;
                
                return true;
            }

            Console.WriteLine("not found");
            return false;
        }

        static byte[] ComputeHashMd5(byte[] dataForHash) => 
            MD5.Create().ComputeHash(dataForHash);

        static byte[] ComputeHashMd5(string str) => 
            MD5.Create().ComputeHash(Encoding.Unicode.GetBytes(str));

        static Guid ComputeGuid(byte[] hash) =>
            new Guid(hash);
        
        public static byte[] ComputeHashSha256(byte[] toBeHashed) => 
            SHA256.Create().ComputeHash(toBeHashed);
        
        public static byte[] ComputeHmacsha1(byte[] toBeHashed, byte[] key) => 
            new HMACSHA1(key).ComputeHash(toBeHashed);
        
        
    }
}