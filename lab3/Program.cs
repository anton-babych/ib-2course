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

            Test test = new Test("0123456789".ToCharArray(), 
                8,
                8, 
                Guid.Parse("564c8da6-0440-88ec-d453-0bbad57c6036"));
            
            Test.TestA();
            
            /*var combs2 = Extension.GetCombinationsAndPerm("0123456789", 8, false);
            
            if(TryFindPasswordFromCombiantions(combs2, out var password)) 
                Console.WriteLine($"Found: password is {password}");*/
        }

        private static bool TryFindPasswordFromCombiantions(List<string> combs, out string password)
        {
            //Guid guidToFind = Guid.Parse("564c8da6-0440-88ec-d453-0bbad57c6036");
            Guid guidToFind = Hashing.ComputeGuid(Hashing.ComputeHashMd5("81539240"));
            
            password = "";
            foreach (var comb in combs)
            {
                //int.TryParse(comb, out var num);
                //var myGuid = ComputeGuid(ComputeHashMd5(BitConverter.GetBytes(num)));
                
                var myGuid = Hashing.ComputeGuid(Hashing.ComputeHashMd5(comb));
                
                //Console.WriteLine($"{myGuid} {myGuid == guidToFind} {guidToFind}");
                
                if (myGuid != guidToFind) continue;
                password = comb;
                
                return true;
            }

            Console.WriteLine("not found");
            return false;
        }


    }
}