using System;
using System.Security.Cryptography;
using System.Text;

namespace lab3
{
    public static class Hashing
    {
        public static byte[] ComputeHashMd5(byte[] dataForHash) => 
            MD5.Create().ComputeHash(dataForHash);

        public static byte[] ComputeHashMd5(string str) => 
            MD5.Create().ComputeHash(Encoding.Unicode.GetBytes(str));

        public static Guid ComputeGuid(byte[] hash) =>
            new Guid(hash);
        
        public static byte[] ComputeHashSha256(byte[] toBeHashed) => 
            SHA256.Create().ComputeHash(toBeHashed);
        
        public static byte[] ComputeHmacsha1(byte[] toBeHashed, byte[] key) => 
            new HMACSHA1(key).ComputeHash(toBeHashed);
    }
}