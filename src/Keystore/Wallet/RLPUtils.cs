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

        public static byte[] EncodeMultiple(byte[] a, byte[] b, byte[] c, List<byte[]> d, List<byte[]> e, List<byte[]> f)
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
            return Utils.Combine(new byte[] { (byte)(0xc0 + length) }, RLP.EncodeElement(a), RLP.EncodeElement(b), RLP.EncodeElement(c), RLP.EncodeList(EncodeElementsBytes(d.ToArray())), RLP.EncodeList(EncodeElementsBytes(e.ToArray())), RLP.EncodeList(EncodeElementsBytes(f.ToArray())));
        }

        public static Multiple DecodeMultiple(byte[] encoderesult)
        {
            Multiple multiple = new Multiple();
            byte[] payloadLen = Utils.CopyByteArray(encoderesult, 0, 1);
            encoderesult = Utils.CopyByteArray(encoderesult, 1, encoderesult.Length - 1);

            int aLength = encoderesult[0] - 0x80;
            byte[] a = Utils.CopyByteArray(encoderesult, 0, aLength + 1);
            multiple.assetHash = RLP.Decode(a).RLPData;
            encoderesult = Utils.CopyByteArray(encoderesult, aLength + 1, encoderesult.Length - (aLength + 1));

            int bLength = encoderesult[0] - 0x80;
            byte[] b = Utils.CopyByteArray(encoderesult, 0, bLength + 1);
            multiple.max = RLP.Decode(b).RLPData.ToIntFromRLPDecoded();
            encoderesult = Utils.CopyByteArray(encoderesult, bLength + 1, encoderesult.Length - (bLength + 1));

            int cLength = encoderesult[0] - 0x80;
            byte[] c = Utils.CopyByteArray(encoderesult, 0, cLength + 1);
            multiple.max = RLP.Decode(b).RLPData.ToIntFromRLPDecoded();
            encoderesult = Utils.CopyByteArray(encoderesult, cLength + 1, encoderesult.Length - (cLength + 1));


            int dLength = encoderesult[0] - 0xc0;
            byte[] d = Utils.CopyByteArray(encoderesult, 0, dLength + 1);
            multiple.pubList = (RLP.Decode(d) as RLPCollection).Select(x => x.RLPData).ToList();
            encoderesult = Utils.CopyByteArray(encoderesult, dLength + 1, encoderesult.Length - (dLength + 1));

            int eLength = encoderesult[0] - 0xc0;
            byte[] e = Utils.CopyByteArray(encoderesult, 0, eLength + 1);
            multiple.signatures = (RLP.Decode(e) as RLPCollection).Select(x => x.RLPData).ToList();

            encoderesult = Utils.CopyByteArray(encoderesult, eLength + 1, encoderesult.Length - (eLength + 1));

            multiple.pubkeyHashList = (RLP.Decode(encoderesult) as RLPCollection).Select(x => x.RLPData).ToList();
            return multiple;
        }


    }
}


