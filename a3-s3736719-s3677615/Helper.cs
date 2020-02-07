using System;
using System.Net.Http;
using System.Net.Http.Headers;
namespace a3_s3736719_s3677615.Helper
{
    public class BankApi
    {
        private const string ApiBaseUri = "https://localhost:44371/";
        public static HttpClient InitializeClient()
        {
            var client = new HttpClient { BaseAddress = new Uri(ApiBaseUri) };
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
