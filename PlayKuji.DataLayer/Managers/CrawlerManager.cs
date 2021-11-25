using PlayKuji.Domain.Interfaces.Managers;
using PlayKuji.Utility.Helpers;

namespace PlayKuji.DataLayer.Managers
{
    public class CrawlerManager : ICrawlerManager
    {
        readonly HttpClientHelper _httpClient;

        public CrawlerManager(
            HttpClientHelper httpClient)
        {
            _httpClient = httpClient;
        }

        public string GetHtml(string url)
        {
            var response = _httpClient.GetAsync(url).GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode)
                return null;

            return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }
    }
}
