using System;

namespace C__SDK
{
    public class KeystoreUtils
    {
        public static String PubkeyToAddress(byte[] pubkey)
        {
            Sha3Keccack sha3Keccack = Sha3Keccack.Current;
            byte[] pub256 = sha3Keccack.CalculateHash(pubkey);
            byte[] r1 = RipemdManager.getHash(pub256);
            byte[] r2 = Utils.AppendByte(r1, (byte)0x00);
            byte[] r3 = sha3Keccack.CalculateHash(sha3Keccack.CalculateHash(r1));
            byte[] b4 = Utils.CopyByteArray(r3, 0, 4);
            byte[] b5 = Utils.Combine(r2, b4);
            String s6 = "WX" + Base58Check.Encode(b5);
            return s6;
        }
    }
}