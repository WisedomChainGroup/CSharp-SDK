using System;
using Xunit;
using Nethereum.Hex.HexConvertors.Extensions;
namespace C__SDK
{
    public class Argons2Test
    {
        [Fact]
        public void TestArgons2WithEverything()
        {
            String password = "123456";
            byte[] salt = new byte[32];
            Argon2Manager argon2Manager = Argon2Manager.Current;
            string result = argon2Manager.hash(System.Text.Encoding.ASCII.GetBytes(password), salt).ToHex();
            Assert.Equal("3cb9d409925ebaf1a9c7700210ce85f1f9bfdc634c32a437144aeccb4d6de577", result);
        }
    }
}
