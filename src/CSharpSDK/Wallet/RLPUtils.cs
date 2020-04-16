using System.Collections.Generic;
using System.Linq;
using System;
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
            int length = RLP.EncodeElement(a).Length + RLP.EncodeElement(b).Length + RLP.EncodeElement(c).Length + RLPUtils.EncodeList(d.ToArray()).Length + RLPUtils.EncodeList(e.ToArray()).Length + RLPUtils.EncodeList(f.ToArray()).Length;
            if (length < 56)
            {
                return Utils.Combine(new byte[] { (byte)(0xc0 + length) }, RLP.EncodeElement(a), RLP.EncodeElement(b), RLP.EncodeElement(c), RLP.EncodeList(EncodeElementsBytes(d.ToArray())), RLP.EncodeList(EncodeElementsBytes(e.ToArray())), RLP.EncodeList(EncodeElementsBytes(f.ToArray())));
            }
            else
            {
                Tuple<byte, byte[]> tuple = NumericsUtils.getLengthByte(length);
                return Utils.Combine(new byte[] { (byte)(0xf7 + tuple.Item1) }, tuple.Item2, RLP.EncodeElement(a), RLP.EncodeElement(b), RLP.EncodeElement(c), RLP.EncodeList(EncodeElementsBytes(d.ToArray())), RLP.EncodeList(EncodeElementsBytes(e.ToArray())), RLP.EncodeList(EncodeElementsBytes(f.ToArray())));
            }
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

            int length = RLP.EncodeElement(origin).Length + RLP.EncodeElement(dest).Length + RLPUtils.EncodeList(from.ToArray()).Length + RLPUtils.EncodeList(signatures.ToArray()).Length + RLP.EncodeElement(to).Length + RLP.EncodeElement(value).Length + RLPUtils.EncodeList(pubkeyHashList.ToArray()).Length;
            if (length < 56)
            {
                return Utils.Combine(new byte[] { (byte)(0xc0 + length) }, RLP.EncodeElement(origin), RLP.EncodeElement(dest), RLP.EncodeList(EncodeElementsBytes(from.ToArray())), RLP.EncodeList(EncodeElementsBytes(signatures.ToArray())), RLP.EncodeElement(to), RLP.EncodeElement(value), RLP.EncodeList(EncodeElementsBytes(pubkeyHashList.ToArray())));
            }
            else
            {
                Tuple<byte, byte[]> tuple = NumericsUtils.getLengthByte(length);
                return Utils.Combine(new byte[] { (byte)(0xf7 + tuple.Item1) }, tuple.Item2, RLP.EncodeElement(origin), RLP.EncodeElement(dest), RLP.EncodeList(EncodeElementsBytes(from.ToArray())), RLP.EncodeList(EncodeElementsBytes(signatures.ToArray())), RLP.EncodeElement(to), RLP.EncodeElement(value), RLP.EncodeList(EncodeElementsBytes(pubkeyHashList.ToArray())));
            }
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
