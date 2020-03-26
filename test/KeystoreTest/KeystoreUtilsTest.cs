using Xunit;
using Nethereum.Hex.HexConvertors.Extensions;

namespace C__SDK
{
    public class KeystoreUtilsTest
    {
        [Fact]
        public void TestKeystoreUtilsWithEverything()
        {
            Assert.Equal("WX12H4655McNe2omWYXGipSr2eZfkpxGCopT", KeystoreUtils.PubkeyHashToAddress("0e015bc15fa1b0156d3f62b16b397d6120faae5b".HexToByteArray()));
            Assert.Equal("WX12H4655McNe2omWYXGipSr2eZfkpxGCopT", KeystoreUtils.PubkeyToAddress("b0b0cdbbaccef0888ac868ea7e9bcd6ec2c7a5b1226628fdf32099297d6a9b8a".HexToByteArray()));
            Assert.Equal("0e015bc15fa1b0156d3f62b16b397d6120faae5b", KeystoreUtils.AddressToPubkeyHash("WX12H4655McNe2omWYXGipSr2eZfkpxGCopT"));
        }
    }

}
