using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TravelWeb.Client.Wrappers;

namespace TravelWeb.Client.Helpers
{
    public class FunctionHelper
    {
        private readonly RestClient _client;

        public FunctionHelper()
        {
            _client = new RestClient();
        }

        public async Task<IEnumerable<T>> Get<T>(string baseUrl, string endpoint)
        {
            try
            {
                _client.BaseUrl = new Uri(baseUrl);
                var request = new RestRequest(endpoint, Method.GET);
                var response = await _client.ExecuteGetAsync(request);
                return JsonConvert.DeserializeObject<List<T>>(response.Content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

            return new List<T>();

        }

        public async Task<PagedResponse<T>> GetPagedResponse<T>(string baseUrl, string endpoint)
        {
            try
            {
                _client.BaseUrl = new Uri(baseUrl);
                var request = new RestRequest(endpoint, Method.GET);
                var response = await _client.ExecuteGetAsync(request);
                return JsonConvert.DeserializeObject<PagedResponse<T>>(response.Content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public async Task<Response<T>> GetResponse<T>(string baseUrl, string endpoint)
        {
            try
            {
                _client.BaseUrl = new Uri(baseUrl);
                var request = new RestRequest(endpoint, Method.GET);
                var response = await _client.ExecuteGetAsync<Response<T>>(request);
                return JsonConvert.DeserializeObject<PagedResponse<T>>(response.Content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Response<T>> PostResponse<T, T1>(string baseUrl, string endpoint, T1 model)
        {
            _client.BaseUrl = new Uri(baseUrl);
            var request = new RestRequest(endpoint, Method.POST);
            request.AddJsonBody(model);
            var result = await _client.ExecutePostAsync(request);
            return JsonConvert.DeserializeObject<Response<T>>(result.Content);
        }

        public async Task<Response<T>> PostAuthorization<T, T1>(string baseUrl, string endpoint, string token ,T1 model)
        {
            _client.BaseUrl = new Uri(baseUrl);
            var request = new RestRequest(endpoint, Method.POST);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddJsonBody(model);
            var result = await _client.ExecutePostAsync(request);
            return JsonConvert.DeserializeObject<Response<T>>(result.Content);
        }
         

        public async Task<IRestResponse> Post<T>(string baseUrl, string endpoint, T model)
        {
            _client.BaseUrl = new Uri(baseUrl);
            var request = new RestRequest(endpoint, Method.POST);
            request.AddJsonBody(model);
            return await _client.ExecuteAsync(request);
        }

    }
}
