using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KooliProjekt.WPF
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _client;
        private const string BaseUrl = "https://localhost:7136/api/";

        public ApiClient()
        {
            var handler = new HttpClientHandler();
            // Игнорируем ошибки SSL (так как у нас самоподписанный сертификат на localhost)
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            _client = new HttpClient(handler);
        }

        public async Task<List<T>> GetAsync<T>(string endpoint)
        {
            var response = await _client.GetAsync(BaseUrl + endpoint);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<T>>(json);
        }

        public async Task PostAsync<T>(string endpoint, T data)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(BaseUrl + endpoint, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task PutAsync<T>(string endpoint, int id, T data)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(BaseUrl + endpoint + "/" + id, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(string endpoint, int id)
        {
            var response = await _client.DeleteAsync(BaseUrl + endpoint + "/" + id);
            response.EnsureSuccessStatusCode();
        }
    }
}
