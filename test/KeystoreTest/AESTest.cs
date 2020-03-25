using System;
using System.IO;
using System.Security.Cryptography;
using Nethereum.Hex.HexConvertors.Extensions;
using Xunit;

namespace C__SDK
{
    public class AESTestcs
    {
        [Fact]
        public void TestAESWithEverything()
        {
            // string original = "Here is some data to encrypt!";

            byte[] plain = new byte[] { 0x01, 0x02 };

            // Create a new instance of the Aes
            // class.  This generates a new key and initialization 
            // vector (IV).
            using (Aes myAes = Aes.Create())
            {
                AesManager aesManager = AesManager.Current;

                byte[] key = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
                byte[] iv = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x05 };
                // Encrypt the string to an array of bytes.
                byte[] encrypted = aesManager.Encryptdata(plain, key, iv);

                // Decrypt the bytes to a string.

                //Display the original data and the decrypted data.
                Assert.Equal("f65f", encrypted.ToHex());
                Assert.Equal(plain, aesManager.Decryptdata(encrypted, key, iv));
            }
        }

    }
}
