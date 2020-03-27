using Xunit;
using Nethereum.Hex.HexConvertors.Extensions;


namespace C__SDK
{
    public class RLPTest
    {
        [Fact]
        public void TestRLPEncodeShortString()
        {
            var test = "dog";
            var expected = "83646f67";
            AssertStringEncoding(test, expected);
            var encoderesult = RLPUtils.EncodeList("123".ToBytesForRLPEncoding(), 1L.ToBytesForRLPEncoding(), 1L.ToBytesForRLPEncoding(), new byte[] { 0x01, 0x01 }, new byte[] { 0x01, 0x01 }, 23.ToBytesForRLPEncoding(), new byte[] { 0x01, 0x01 });
            Assert.Equal("d083313233010182010182010117820101", encoderesult.ToHex());
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
