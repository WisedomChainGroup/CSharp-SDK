using System;
using Xunit;
using Nethereum.Hex.HexConvertors.Extensions;
namespace C__SDK
{
    public class RipemdManagerTest
    {
        [Fact]
        public void TestRipemd160WithEverything()
        {
            Assert.Equal("189f7c8b1a386ffe8eed91b3830c7a7bcd1e778c",RipemdManager.getHash(new byte[]{0x01,0x02}).ToHex());
        }
    }
}
