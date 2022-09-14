using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Cryptography
{
    public static class HashData
    {
        // by default we dont work with string or file, anything to br hashed is usually 
        //converted to a byte array 
        public static byte[] ComputeHashSha1(byte[] toBeHashed)
        {
            //the create  method creates an instance of the default implemantation of SHA1
            using var sha1 = SHA1.Create();
            return sha1.ComputeHash(toBeHashed);
           
        }

        public static byte[] ComputeHashSha256(byte[] toBeHashed)
        {
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(toBeHashed);
        }

        public static byte[] ComputeHashSha512(byte[] toBeHashed)
        {
            using var sha512 = SHA512.Create();
            return sha512.ComputeHash(toBeHashed);
        }

        public static byte[] ComputeHashMd5(byte[] toBeHashed)
        {
            using var md5 = MD5.Create();
            return md5.ComputeHash(toBeHashed);
        }
    }
}
