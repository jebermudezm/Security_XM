using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace XM.Security.Classes
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private const string SUBSCRIPTIONKEY = "34361280d7bd4903bbb414d81586e7aa";
        private const string CORRELATIONID = "xxx";

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<JObject> ExecuteApiRequest(string url, HttpMethod method,  string bearerToken, HttpContent content = null)
        {
            var request = new HttpRequestMessage(method, url);

            // Añadir los headers
            request.Headers.Add("Ocp-Apim-Subscription-Key", SUBSCRIPTIONKEY);
            request.Headers.Add("Authorization", $"Bearer {bearerToken}");
            request.Headers.Add("idCorrelacion", CORRELATIONID);

            if (content != null)
            {
                request.Content = content;
            }

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            try
            {
                return JObject.Parse(responseData);
            }
            catch (JsonReaderException ex)
            {
                Console.WriteLine("Error al deserializar la respuesta: " + ex.Message);
                throw;
            }
        }

    }
}
