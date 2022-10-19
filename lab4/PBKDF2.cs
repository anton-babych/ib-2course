using System;
using lab3;

namespace lab4
{
    public class PBKDF2
    {
        public enum HashingAlgorithm{
            MD5, SHA1, SHA256, SHA384, SHA512
        }

        private HashingAlgorithm _algorithm;

        public PBKDF2(HashingAlgorithm algorithm)
        {
            _algorithm = algorithm;
        }

        private byte[] GetHash(byte[] input)
        {
            switch (_algorithm)
            {
                case HashingAlgorithm.MD5:
                    return Hashing.ComputeHashMd5(input);
                    
                case HashingAlgorithm.SHA1:
                    return Hashing.ComputeHashSha1(input);
                
                case HashingAlgorithm.SHA256:
                    return Hashing.ComputeHashSha256(input);
                
                case HashingAlgorithm.SHA384:
                    return Hashing.ComputeHashSha384(input);;
                
                case HashingAlgorithm.SHA512:
                    return Hashing.ComputeHashSha512(input);
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}