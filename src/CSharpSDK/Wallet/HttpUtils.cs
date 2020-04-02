using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CSharp_SDK
{

    public class HttpUtils
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<string> sendGet(string url)
        {
            var responseString = await client.GetStringAsync(url);
            return responseString;
        }

        public static async Task<string> sendPost(string url,Dictionary<string, string> pairs)
        {
            var content = new FormUrlEncodedContent(pairs);
            var response = await client.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }
    }
}
