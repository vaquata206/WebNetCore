using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebClient.Core.Helper
{
    public class HttpHelper: IDisposable
    {
        public HttpClient Client { get; private set; }

        public HttpHelper(): this(WebConfig.ApiSystemUrl)
        {
        }

        public HttpHelper(string uri)
        {
            this.Client = new HttpClient();
            this.Client.BaseAddress = new Uri(uri);
            this.Client.DefaultRequestHeaders.Accept.Clear();
            this.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpResponseMessage> PostAsync(string jsonString, string uri)
        {
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            return await this.Client.PostAsync(uri, content);
        }

        public void Dispose()
        {
            this.Client.Dispose();
        }
    }
}
