using System.Collections.Generic;
using System.Linq;
namespace CSharp_SDK
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

        public static Multiple DecodeMultiple(byte[] encodeResult)
        {
            Multiple multiple = new Multiple();
            byte[] payloadLen = Utils.CopyByteArray(encodeResult, 0, 1);
            encodeResult = Utils.CopyByteArray(encodeResult, 1, encodeResult.Length - 1);
            var collections = RLP.Decode(encodeResult) as RLPCollection;
            multiple.assetHash = collections[0].RLPData;
            multiple.max = collections[1].RLPData.ToIntFromRLPDecoded();
            multiple.min = collections[2].RLPData.ToIntFromRLPDecoded();
            multiple.pubList = (collections[3] as RLPCollection).Select(x => x.RLPData).ToList();
            multiple.signatures = (collections[4] as RLPCollection).Select(x => x.RLPData).ToList();
            multiple.pubkeyHashList = (collections[5] as RLPCollection).Select(x => x.RLPData).ToList();
            return multiple;
        }

        public static byte[] EncodeMultTransfer(byte[] origin, byte[] dest, List<byte[]> from, List<byte[]> signatures, byte[] to, byte[] value, List<byte[]> pubkeyHashList)
        {
            int length = origin.Length + dest.Length + to.Length + value.Length + from.Select(x => x.Length + 1).Sum() + signatures.Select(x => x.Length + 1).Sum() + pubkeyHashList.Select(x => x.Length + 1).Sum() + 3;
            if (origin.Length > 1)
            {
                length++;
            }
            if (dest.Length > 1)
            {
                length++;
            }
            if (to.Length > 1)
            {
                length++;
            }
            if (value.Length > 1)
            {
                length++;
            }
            return Utils.Combine(new byte[] { (byte)(0xc0 + length) }, RLP.EncodeElement(origin), RLP.EncodeElement(dest), RLP.EncodeList(EncodeElementsBytes(from.ToArray())), RLP.EncodeList(EncodeElementsBytes(signatures.ToArray())), RLP.EncodeElement(to), RLP.EncodeElement(value), RLP.EncodeList(EncodeElementsBytes(pubkeyHashList.ToArray())));
        }

        public static MultTransfer DecodeMultTransfer(byte[] encodeResult)
        {
            MultTransfer multTransfer = new MultTransfer();
            byte[] payloadLen = Utils.CopyByteArray(encodeResult, 0, 1);
            encodeResult = Utils.CopyByteArray(encodeResult, 1, encodeResult.Length - 1);
            var collections = RLP.Decode(encodeResult) as RLPCollection;
            multTransfer.origin = collections[0].RLPData.ToIntFromRLPDecoded();
            multTransfer.dest = collections[1].RLPData.ToIntFromRLPDecoded();
            multTransfer.from = (collections[2] as RLPCollection).Select(x => x.RLPData).ToList();
            multTransfer.signatures = (collections[3] as RLPCollection).Select(x => x.RLPData).ToList();
            multTransfer.to = collections[4].RLPData;
            multTransfer.value = collections[5].RLPData.ToIntFromRLPDecoded();
            multTransfer.pubkeyHashList = (collections[6] as RLPCollection).Select(x => x.RLPData).ToList();
            return multTransfer;
        }

        public static Asset DecodeAsset(byte[] encodeResult)
        {
            Asset asset = new Asset();
            byte[] payloadLen = Utils.CopyByteArray(encodeResult, 0, 1);
            encodeResult = Utils.CopyByteArray(encodeResult, 1, encodeResult.Length - 1);
            var collections = RLP.Decode(encodeResult) as RLPCollection;
            asset.code = collections[0].RLPData.ToStringFromRLPDecoded();
            asset.offering = collections[1].RLPData.ToLongFromRLPDecoded();
            asset.totalAmount = collections[2].RLPData.ToLongFromRLPDecoded();
            asset.createUser = collections[3].RLPData;
            asset.owner = collections[4].RLPData;
            asset.allowIncrease = collections[5].RLPData.ToIntFromRLPDecoded();
            asset.info = collections[6].RLPData;
            return asset;
        }

    }
}
