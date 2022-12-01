
using System;
using System.Text;
using lab1;

namespace lab7
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Task1();
        }

        private static void Task1()
        {
            var eds = new Eds();

            string publicKeyPath = "./publicKey.xml";

            string message = "testing.";
            byte[] sign = eds.SignMessage(publicKeyPath, message);
            
            string fakeMessage = "i'm fake";
            
            bool signCheck = eds.Verify(publicKeyPath, Encoding.Unicode.GetBytes(message), sign);
            Console.WriteLine($"{message} are " + (signCheck ? "same" : "not same"));
            
            signCheck = eds.Verify(publicKeyPath, Encoding.Unicode.GetBytes(fakeMessage), sign);
            Console.WriteLine($"{fakeMessage} are " + (signCheck ? "same" : "not same"));
        }
    }
}