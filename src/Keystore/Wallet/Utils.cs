using System.Linq;
using System;

namespace C__SDK
{
    public class Utils
    {
        public static byte[] Combine(params byte[][] arrays)
        {
            byte[] rv = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in arrays)
            {
                System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }

        /// <summary>
        ///     Creates a copy of bytes and appends b to the end of it
        /// </summary>
        public static byte[] AppendByte(byte[] bytes, byte b)
        {
            var result = new byte[bytes.Length + 1];
            Array.Copy(bytes, result, bytes.Length);
            result[result.Length - 1] = b;
            return result;
        }

        public static byte[] CopyByteArray(byte[] s, int start, int count)
        {
            byte[] r = new byte[count];
            Array.Copy(s, start, r, 0, count);
            return r;
        }
    }
}