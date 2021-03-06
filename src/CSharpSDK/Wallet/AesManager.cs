using System;
using CS_AES_CTR;

namespace CSharp_SDK
{
    public class AesManager
    {

        public static AesManager Current { get; } = new AesManager();

        public static String cipher = "aes-256-ctr";

        public byte[] Encryptdata(byte[] plain, byte[] key, byte[] iv)
        {
            // Encrypt
            AES_CTR forEncrypting = new AES_CTR(key, iv);
            byte[] encrypted = new byte[plain.Length];
            forEncrypting.EncryptBytes(encrypted, plain);
            return encrypted;
        }

        public byte[] Decryptdata(byte[] encrypted, byte[] key, byte[] iv)
        {
            AES_CTR forEncrypting = new AES_CTR(key, iv);
            byte[] plain = new byte[encrypted.Length];
            forEncrypting.DecryptBytes(plain, encrypted);
            return plain;
        }

    }
}
