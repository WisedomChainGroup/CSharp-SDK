using DevHawk.Security.Cryptography;
using System;
namespace C__SDK
{
    public class RipemdManager
    { 

        public static byte[] getHash(byte[] plain)
        {
            Lazy<RIPEMD160> ripemd160 = new Lazy<RIPEMD160>(() => new RIPEMD160());
            return ripemd160.Value.ComputeHash(plain);
        }
    }
}
