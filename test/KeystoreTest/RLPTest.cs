using Xunit;
using Nethereum.Hex.HexConvertors.Extensions;
using System.Collections.Generic;
using System.Linq;


namespace C__SDK
{
    public class RLPTest
    {
        [Fact]
        public void TestRLPEncodeShortString()
        {

            List<byte[]> list = new List<byte[]>();
            list.Add(new byte[] { 0x01, 0x02 });
            list.Add(new byte[] { 0x01, 0x03 });
            var encoderesult = RLPUtils.EncodeMultiple(new byte[] { 0x01, 0x02 }, new byte[] { 0x01, 0x02 }, new byte[] { 0x01, 0x02 }, list, new List<byte[]>(), list);

            byte[] payloadLen = Utils.CopyByteArray(encoderesult, 0, 1);
            encoderesult = Utils.CopyByteArray(encoderesult, 1, encoderesult.Length - 1);

            int aLength = encoderesult[0] - 0x80;
            byte[] a = Utils.CopyByteArray(encoderesult, 0, aLength + 1);
            byte[] a1 = RLP.Decode(a).RLPData;
            encoderesult = Utils.CopyByteArray(encoderesult, aLength + 1, encoderesult.Length - (aLength + 1));

            int bLength = encoderesult[0] - 0x80;
            byte[] b = Utils.CopyByteArray(encoderesult, 0, bLength + 1);
            byte[] b1 = RLP.Decode(b).RLPData;
            encoderesult = Utils.CopyByteArray(encoderesult, bLength + 1, encoderesult.Length - (bLength + 1));

            int cLength = encoderesult[0] - 0x80;
            byte[] c = Utils.CopyByteArray(encoderesult, 0, cLength + 1);
            byte[] c1 = RLP.Decode(b).RLPData;
            encoderesult = Utils.CopyByteArray(encoderesult, cLength + 1, encoderesult.Length - (cLength + 1));


            int dLength = encoderesult[0] - 0xc0;
            byte[] d = Utils.CopyByteArray(encoderesult, 0, dLength + 1);
            var d1 = (RLP.Decode(d) as RLPCollection).Select(x => x.RLPData).ToList();
            encoderesult = Utils.CopyByteArray(encoderesult, dLength + 1, encoderesult.Length - (dLength + 1));

            int eLength = encoderesult[0] - 0xc0;
            byte[] e = Utils.CopyByteArray(encoderesult, 0, eLength + 1);
            var e1 = (RLP.Decode(e) as RLPCollection).Select(x => x.RLPData).ToList();

            encoderesult = Utils.CopyByteArray(encoderesult, eLength + 1, encoderesult.Length - (eLength + 1));


            var f1 = (RLP.Decode(encoderesult) as RLPCollection).Select(x => x.RLPData).ToList();
            Assert.Equal("0103", f1[1].ToHex());
        }

        private static void AssertStringCollection(string[] test, string expected)
        {
            var encoderesult = RLP.EncodeList(RLPUtils.EncodeElementsBytes(test.ToBytesForRLPEncoding()));
            Assert.Equal(expected, encoderesult.ToHex());

            var decodeResult = RLP.Decode(encoderesult) as RLPCollection;
            for (var i = 0; i < test.Length; i++)
                Assert.Equal(test[i], decodeResult[i].RLPData.ToStringFromRLPDecoded());
        }

        private static void AssertStringEncoding(string test, string expected)
        {
            var testBytes = test.ToBytesForRLPEncoding();
            var encoderesult = RLP.EncodeElement(testBytes);
            Assert.Equal(expected, encoderesult.ToHex());

            var decodeResult = RLP.Decode(encoderesult).RLPData;
            Assert.Equal(test, decodeResult.ToStringFromRLPDecoded());
        }

    }
}
