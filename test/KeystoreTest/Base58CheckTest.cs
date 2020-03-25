using Xunit;
namespace C__SDK
{
    public class Base58CheckTest
    {
        [Fact]
        public void TestBase58WithEverything()
        {
            Assert.Equal("5T", Base58Check.Encode(new byte[] { 0x01, 0x02 }));
            Assert.Equal(new byte[] { 0x01, 0x02 }, Base58Check.Decode("5T"));
        }
    }

}