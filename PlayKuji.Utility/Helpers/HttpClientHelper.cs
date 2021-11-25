using System.Net.Http;
using System.Threading.Tasks;

namespace PlayKuji.Utility.Helpers
{
    public class HttpClientHelper
    {
        readonly IHttpClientFactory _clientFactory;

        public HttpClientHelper(
            IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            var httpClient = _clientFactory.CreateClient();
            return await httpClient.GetAsync(url);
        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent httpContent)
        {
            var httpClient = _clientFactory.CreateClient();
            return await httpClient.PostAsync(url, httpContent);
        }
    }
}