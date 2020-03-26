using System;
using Nethereum.Hex.HexConvertors.Extensions;

namespace C__SDK
{
    public class KeystoreUtils
    {
        public static String PubkeyToAddress(byte[] pubkey)
        {
            Sha3Keccack sha3Keccack = Sha3Keccack.Current;
            byte[] pub256 = sha3Keccack.CalculateHash(pubkey);
            byte[] r1 = RipemdManager.getHash(pub256);
            return PubkeyHashToAddress(r1);
        }

          public static string PubkeyHashToAddress(byte[] publicHash)
        {
            Sha3Keccack sha3Keccack = Sha3Keccack.Current;
            byte[] r1 = publicHash;
            byte[] r2 = Utils.prepend(r1, (byte)0x00);
            byte[] r3 = sha3Keccack.CalculateHash(sha3Keccack.CalculateHash(r1));
            byte[] b4 = Utils.CopyByteArray(r3, 0, 4);
            byte[] b5 = Utils.Combine(r2, b4);
            String s6 = "WX" + Base58Check.Encode(b5);
            return s6;
        }

        public static string PubkeyHashToAddress(string publicHash)
        {
            return PubkeyHashToAddress(publicHash.HexToByteArray());
        }

        private static byte[] AddressToPubkeyHashByteArray(string address)
        {
            byte[] r5 = Base58Check.Decode(address.Substring(2));
            byte[] r2 = Utils.CopyByteArray(r5, 0, 21);
            byte[] r1 = Utils.CopyByteArray(r2, 1, 20);
            return r1;
        }

        public static string AddressToPubkeyHash(string address)
        {
            return AddressToPubkeyHashByteArray(address).ToHex();
        }

    }
}