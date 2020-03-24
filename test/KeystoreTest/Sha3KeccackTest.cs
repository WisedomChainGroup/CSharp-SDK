using Xunit;
namespace C__SDK
{
    public class Sha3KeccackTest
    {
        [Fact]
        public void TestSha3KeccackWithEverything()
        {
            Sha3Keccack sha3Keccack = Sha3Keccack.Current;
            Assert.Equal("b8f132fb6526e0405f3ce4f3bab301f1d4409b1e7f2c01c2037d6cf845c831cb", sha3Keccack.CalculateHash("LogCreate(address)"));
        }
    }

}
