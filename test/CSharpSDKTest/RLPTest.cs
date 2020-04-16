using Xunit;
using Nethereum.Hex.HexConvertors.Extensions;
using System.Collections.Generic;
using System.Linq;


namespace CSharp_SDK
{
    public class RLPTest
    {
        [Fact]
        public void TestRLPEncodeShortString()
        {

            List<byte[]> list = new List<byte[]>();
            list.Add(new byte[] { 0x01, 0x02 });
            list.Add(new byte[] { 0x01, 0x03 });
            var encodeResult = RLPUtils.EncodeMultiple(new byte[] { 0x01, 0x02 }, new byte[] { 0x01, 0x02 }, new byte[] { 0x01, 0x02 }, list, new List<byte[]>(), list);
            var collections = RLP.Decode(encodeResult) as RLPCollection;
            Assert.Equal("0102", collections[0].RLPData.ToHex());
            Assert.Equal("0102", collections[1].RLPData.ToHex());
            Assert.Equal("0102", collections[2].RLPData.ToHex());
            Assert.Equal(list, (collections[3] as RLPCollection).Select(x => x.RLPData).ToList());
            Assert.Equal(new List<byte[]>(), (collections[4] as RLPCollection).Select(x => x.RLPData).ToList());
            Assert.Equal(list, (collections[5] as RLPCollection).Select(x => x.RLPData).ToList());
        }

    }
}
