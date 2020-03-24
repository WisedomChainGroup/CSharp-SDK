using Xunit;
using System.Linq;
using System.Text;
using Nethereum.Hex.HexConvertors.Extensions;
using Org.BouncyCastle.Crypto.Digests;
namespace C__SDK
{
    public class Sha3KeccackTest
    {
        [Fact]
        public void TestSha3KeccackWithEverything()
        {
            Assert.Equal("b8f132fb6526e0405f3ce4f3bab301f1d4409b1e7f2c01c2037d6cf845c831cb", CalculateHash("LogCreate(address)"));
        }

         public static Sha3KeccackTest Current { get; } = new Sha3KeccackTest();

        public string CalculateHash(string value)
        {
            var input = Encoding.UTF8.GetBytes(value);
            var output = CalculateHash(input);
            return output.ToHex();
        }

        public string CalculateHashFromHex(params string[] hexValues)
        {
            var joinedHex = string.Join("", hexValues.Select(x => x.RemoveHexPrefix()).ToArray());
            return CalculateHash(joinedHex.HexToByteArray()).ToHex();
        }

        public byte[] CalculateHash(byte[] value)
        {
            var digest = new KeccakDigest(256);
            var output = new byte[digest.GetDigestSize()];
            digest.BlockUpdate(value, 0, value.Length);
            digest.DoFinal(output, 0);
            return output;
        }
    }

}
