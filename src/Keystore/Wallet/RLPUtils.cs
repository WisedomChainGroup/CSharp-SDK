using System.Collections.Generic;
using System.Linq;
namespace C__SDK
{
    public class RLPUtils
    {
        public static byte[][] EncodeElementsBytes(params byte[][] bytes)
        {
            var encodeElements = new List<byte[]>();
            foreach (var byteElement in bytes)
                encodeElements.Add(RLP.EncodeElement(byteElement));
            return encodeElements.ToArray();
        }

        public static byte[] EncodeList(params byte[][] bytes)
        {
            return RLP.EncodeList(EncodeElementsBytes(bytes));
        }

        public static byte[] EncodeList(byte[] a, byte[] b, byte[] c, List<byte[]> d, List<byte[]> e, List<byte[]> f)
        {
            int length = a.Length + b.Length + c.Length + d.Select(x => x.Length + 1).Sum() + e.Select(x => x.Length + 1).Sum() + f.Select(x => x.Length + 1).Sum() + 3;
            if (a.Length > 1)
            {
                length++;
            }
            if (b.Length > 1)
            {
                length++;
            }
            if (c.Length > 1)
            {
                length++;
            }
            return Utils.Combine(new byte[] { (byte)(0xc0 + length) }, RLP.EncodeElement(a), RLP.EncodeElement(b), RLP.EncodeList(EncodeElementsBytes(d.ToArray())), RLP.EncodeList(EncodeElementsBytes(e.ToArray())), RLP.EncodeList(EncodeElementsBytes(f.ToArray())));
        }


    }
}


