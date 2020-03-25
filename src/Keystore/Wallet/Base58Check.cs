using SimpleBase;

namespace C__SDK
{
    public class Base58Check
    {

        public static string Encode(byte[] plain)
        {
            return Base58.Bitcoin.Encode(plain);
        }

        public static byte[] Decode(string encrypted)
        {
            return Base58.Bitcoin.Decode(encrypted).ToArray();
        }
    }
}
