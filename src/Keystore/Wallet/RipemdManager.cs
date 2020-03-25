using System.Security.Cryptography;

namespace C__SDK
{
    public class RipemdManager
    {

        public static byte[] getHash(byte[] plain)
        {
            // create a ripemd160 object
            RIPEMD160 r160 = RIPEMD160Managed.Create();
            return r160.ComputeHash(plain);
        }
    }
}