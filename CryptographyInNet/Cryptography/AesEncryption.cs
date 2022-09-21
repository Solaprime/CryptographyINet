using System;
using System.Collections.Generic;
using System.IO;
//using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;

namespace Cryptography
{
    public  class AesEncryption
    {
        //genrate random number
        public byte[] GenerateRandomNumber(int length)
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[length];
                randomNumberGenerator.GetBytes(randomNumber);

                return randomNumber;
            }
        }

        // Encrypt  flow, we convert the data we want to encrypt to byte array,
        // we conveertt the key to a byte array as well
        // we convert the Iv intialzation vector to abyte array as well,
        // the iv isnt a secret 

        public byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            //instanciate the AesCrytoserviceprovider
            using (var aes = new AesCryptoServiceProvider())
            {
                // the mode and padding are set to their default,
                //in most cases the deafault are okay though
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                //we pass our key and Iv 
                aes.Key = key;
                aes.IV = iv;

                //Aes uses stream so where create a new stream
                using (var memoryStream = new MemoryStream())
                {
                    // we chain the stream to a crypto stream
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(),
                        CryptoStreamMode.Write);

                    //we write our stream
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    // we flush out the stream
                    cryptoStream.FlushFinalBlock();

                    return memoryStream.ToArray();
                }
            }
        }

        // similar to the Encrypt flow
        public byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
        {
            
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                aes.Key = key;
                aes.IV = iv;

                using (var memoryStream = new MemoryStream())
                {
                    // In abpve we called createEncryptor but here we cll createDecryptor
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(),
                        CryptoStreamMode.Write);

                    cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                    cryptoStream.FlushFinalBlock();

                    var decryptBytes = memoryStream.ToArray();

                    // return decrypt data
                    return decryptBytes;
                }
            }
        }
    }
}
