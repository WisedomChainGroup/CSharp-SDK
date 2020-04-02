using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace CSharp_SDK
{
    public class HttpUtilsTest
    {
        [Fact]
        public void TestHttpGetRequest()
        {
            Task<string> result = HttpUtils.sendGet("http://192.168.1.12:19585/height");
            Assert.Contains("data", result.Result);
        }

        /**
               send transaction, such as
               var values = new Dictionary<string, string> { { "traninfo", "xxxxxxxxxxxxxxxxxxxxxxxxxxxxx" } };
               Task<string> result = HttpUtils.sendPost("http://192.168.1.12:19585/sendTransaction", values);
           **/
        [Fact]
        public void TestHttpPostRequest()
        {
            var values = new Dictionary<string, string> { { "pubkeyhash", "ee649fbd62ee91dce16017152c94acdaa11abe86" } };
            Task<string> result = HttpUtils.sendPost("http://192.168.1.12:19585/sendBalance", values);
            Assert.Contains("data", result.Result);

        }
    }
}
