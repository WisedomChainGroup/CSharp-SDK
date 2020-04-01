using Xunit;
using Nethereum.Hex.HexConvertors.Extensions;

namespace CSharp_SDK
{
    public class KeystoreUtilsTest
    {
        [Fact]
        public void TestKeystoreUtilsWithEverything()
        {
            Assert.Equal("WX12H4655McNe2omWYXGipSr2eZfkpxGCopT", KeystoreUtils.PubkeyHashToAddress("0e015bc15fa1b0156d3f62b16b397d6120faae5b".HexToByteArray()));
            Assert.Equal("WX12H4655McNe2omWYXGipSr2eZfkpxGCopT", KeystoreUtils.PubkeyToAddress("b0b0cdbbaccef0888ac868ea7e9bcd6ec2c7a5b1226628fdf32099297d6a9b8a".HexToByteArray()));
            Assert.Equal("0e015bc15fa1b0156d3f62b16b397d6120faae5b", KeystoreUtils.AddressToPubkeyHash("WX12H4655McNe2omWYXGipSr2eZfkpxGCopT"));
            Assert.Equal("b0b0cdbbaccef0888ac868ea7e9bcd6ec2c7a5b1226628fdf32099297d6a9b8a", KeystoreUtils.PrivatekeyToPublicKey("cee16fca065611da7889f7069fa0ba245a24b4d0345d8d74c78a3601180fafbc"));
        }
    }

}
