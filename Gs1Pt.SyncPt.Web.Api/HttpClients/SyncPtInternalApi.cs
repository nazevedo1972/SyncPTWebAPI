using Gs1Pt.SyncPt.Web.Api.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace Gs1Pt.SyncPt.Web.Api.HttpClients
{
    public class SyncPtInternalApi : ISyncPtInternalApi
    {
        private readonly HttpClient _client;

        public SyncPtInternalApi(HttpClient client)
        {
            _client = client;
            _client.Timeout = TimeSpan.FromMinutes(5);
        }

        public async Task<T> GetAsync<T>(string requestUri)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            var httpResponse = await _client.GetAsync($"{requestUri}");
            return JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync(), settings);
        }

        public async Task<T> GetAsync<T>(string requestUri, List<KeyValuePair<string, string>> requestHeaders = null)
        {
            if (requestHeaders.IsNotNullOrEmpty())
            {
                foreach (var requestHeader in requestHeaders)
                {
                    _client.DefaultRequestHeaders.Add(requestHeader.Key, requestHeader.Value);
                }
            }
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            var httpResponse = await _client.GetAsync($"{requestUri}");
            return JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync(), settings);
        }

        public async Task<T2> PostAsync<T1, T2>(string requestUri, T1 body, List<KeyValuePair<string, string>> requestHeaders = null)
        {
            if (requestHeaders.IsNotNullOrEmpty())
            {
                foreach (var requestHeader in requestHeaders)
                {
                    _client.DefaultRequestHeaders.Add(requestHeader.Key, requestHeader.Value);
                }
            }
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            string content = JsonConvert.SerializeObject(body, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var httpResponse = await _client.PostAsync($"{requestUri}", new StringContent(content, Encoding.Default, "application/json"));
            return JsonConvert.DeserializeObject<T2>(await httpResponse.Content.ReadAsStringAsync(), settings);
        }

        public async Task<T2> PutAsync<T1, T2>(string requestUri, T1 body, List<KeyValuePair<string, string>> requestHeaders = null)
        {
            if (requestHeaders.IsNotNullOrEmpty())
            {
                foreach (var requestHeader in requestHeaders)
                {
                    _client.DefaultRequestHeaders.Add(requestHeader.Key, requestHeader.Value);
                }
            }
              var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            string content = JsonConvert.SerializeObject(body, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var httpResponse = await _client.PutAsync($"{requestUri}", new StringContent(content, Encoding.Default, "application/json"));
            return JsonConvert.DeserializeObject<T2>(await httpResponse.Content.ReadAsStringAsync(), settings);
        }
    }

     public class SyncPtInternalApiJobs : ISyncPtInternalApiJobs
    {
        private readonly HttpClient _client;

        public SyncPtInternalApiJobs(HttpClient client)
        {
            _client = client;
            _client.Timeout = TimeSpan.FromMinutes(5);
        }

        public async Task<T> GetAsync<T>(string requestUri)
        {
            var httpResponse = await _client.GetAsync($"{requestUri}");
            return JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<T2> PostAsync<T1, T2>(string requestUri, T1 body, List<KeyValuePair<string, string>> requestHeaders = null)
        {
            if (requestHeaders.IsNotNullOrEmpty())
            {
                foreach (var requestHeader in requestHeaders)
                {
                    _client.DefaultRequestHeaders.Add(requestHeader.Key, requestHeader.Value);
                }
            }

            string content = JsonConvert.SerializeObject(body, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var httpResponse = await _client.PostAsync($"{requestUri}", new StringContent(content, Encoding.Default, "application/json"));
            return JsonConvert.DeserializeObject<T2>(await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<T2> PutAsync<T1, T2>(string requestUri, T1 body, List<KeyValuePair<string, string>> requestHeaders = null)
        {
            if (requestHeaders.IsNotNullOrEmpty())
            {
                foreach (var requestHeader in requestHeaders)
                {
                    _client.DefaultRequestHeaders.Add(requestHeader.Key, requestHeader.Value);
                }
            }

            string content = JsonConvert.SerializeObject(body, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            var httpResponse = await _client.PutAsync($"{requestUri}", new StringContent(content, Encoding.Default, "application/json"));
            return JsonConvert.DeserializeObject<T2>(await httpResponse.Content.ReadAsStringAsync());
        }
    }
}
