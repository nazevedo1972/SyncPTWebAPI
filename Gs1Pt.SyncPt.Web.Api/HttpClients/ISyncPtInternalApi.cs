namespace Gs1Pt.SyncPt.Web.Api.HttpClients
{
    public interface ISyncPtInternalApi
    {
        Task<T> GetAsync<T>(string requestUri);
        Task<T2> PostAsync<T1, T2>(string requestUri, T1 body, List<KeyValuePair<string, string>> requestHeaders = null);
        Task<T2> PutAsync<T1, T2>(string requestUri, T1 body, List<KeyValuePair<string, string>> requestHeaders = null);
        Task<T> GetAsync<T>(string requestUri, List<KeyValuePair<string, string>> requestHeaders = null);
    }

    public interface ISyncPtInternalApiJobs
    {
        Task<T> GetAsync<T>(string requestUri);
        Task<T2> PostAsync<T1, T2>(string requestUri, T1 body, List<KeyValuePair<string, string>> requestHeaders = null);
        Task<T2> PutAsync<T1, T2>(string requestUri, T1 body, List<KeyValuePair<string, string>> requestHeaders = null);
    }
}
