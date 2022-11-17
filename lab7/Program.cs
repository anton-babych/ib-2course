
using System;
using System.Text;

namespace lab7
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("cagqjng");
            Task1();
        }

        private static void Task1()
        {
            var eds = new Eds();

            string publicKeyPath = "./publicKey.xml";

            string message = "testing.";
            byte[] sign = eds.SignMessage(publicKeyPath, message);
            
            string anotherMessage = "i'm fake";
            byte[] anotherSign = eds.SignMessage(publicKeyPath, anotherMessage);

            bool signCheck = eds.Compare(publicKeyPath, sign, anotherSign);
            Console.WriteLine("keys are " + (signCheck ? "same" : "not same"));
        }
    }
}