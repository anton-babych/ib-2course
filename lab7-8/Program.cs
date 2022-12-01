using System;
using System.Text;

namespace lab7
{
    internal class Program
    {
        private const string ProgramPath = "C:/Users/Abob/RiderProjects/Ib-2kurs/lab7-8/";
        
        public static void Main(string[] args)
        {
            Task3();
        }

        private static void Task1()
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes("test");
            
            RsaTest rsaTest = new RsaTest();
            rsaTest.AssignNewKey();
            
            byte[] encryptedMessage = rsaTest.EncryptFromMemory(messageBytes);
            byte[] decryptedMessage = rsaTest.DecryptFromMemory(encryptedMessage);

            Console.WriteLine(Encoding.UTF8.GetString(decryptedMessage));
        }
        
        private static void Task2()
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes("test");
            string keyPath = ProgramPath + "privatekey.xml";
            
            RsaTest rsaTest = new RsaTest();
            rsaTest.AssignNewKey(keyPath);
            
            byte[] encryptedMessage = rsaTest.EncryptFromFile(keyPath, messageBytes);
            byte[] decryptedMessage = rsaTest.DecryptFromMemory(encryptedMessage);

            Console.WriteLine(Encoding.UTF8.GetString(decryptedMessage));
        }

        private static void Task3()
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes("test");
            string keyPath = ProgramPath + "babych.xml";
            
            RsaTest rsaTest = new RsaTest();
            rsaTest.AssignNewKey(keyPath);

            //byte[] encryptedMessage = rsaTest.EncryptFromFile(keyPath, messageBytes);
            //byte[] decryptedMessage = rsaTest.DecryptFromContainer(encryptedMessage);

            //Console.WriteLine(Encoding.UTF8.GetString(decryptedMessage));
        }
    }
}