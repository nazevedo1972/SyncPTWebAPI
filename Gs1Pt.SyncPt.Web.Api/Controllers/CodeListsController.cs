using Gs1Pt.SyncPt.Domain.Entities.RestApi.V1.CodeLists;
using Gs1Pt.SyncPt.Web.Api.Extensions;
using Gs1Pt.SyncPt.Web.Api.HttpClients;
using Gs1Pt.SyncPt.Web.Api.Models.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
 

namespace Gs1Pt.SyncPt.Web.Api.Controllers
{
    [Authorize]
    [ApiVersion("3.0")]
    [ApiController]
    [Route("api/v3/[controller]")]
    public class CodeListsController : ControllerBase
    {
        private readonly ISyncPtInternalApi _syncPtInternalApi;
        private readonly IMemoryCache _memoryCache;
        private const string APIPREFIX = "api/v1/codelists";

        public CodeListsController(ISyncPtInternalApi syncPtInternalApi)
        {
            _syncPtInternalApi = syncPtInternalApi;
        }

        [HttpGet(Name = "GetCodeListsAsync")]
        public async Task<IEnumerable<CodeList>> GetCodeListsAsync()
        {
            var requestHeaders = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Username", User.GetUsername()),
            };
            return await _syncPtInternalApi.GetAsync<IEnumerable<CodeList>>($"{APIPREFIX}/api");
        }

        [HttpGet("items", Name = "GetCodeListItemsAsync")]
        public async Task<IEnumerable<CodeListItem>> GetCodeListItemsAsync(string codeListName)
        {
            var requestHeaders = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Username", User.GetUsername()),
            };
            return await _syncPtInternalApi.GetAsync<IEnumerable<CodeListItem>>($"{APIPREFIX}/api/items/{codeListName}/{"3.1"}/{TradeItemConstants.TRADEITEM_LANGUAGE_DEFAULT}",SetRequestHeaders());
        }

        private List<KeyValuePair<string, string>> SetRequestHeaders()
        {
            var requestHeaders = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Username", User.GetUsername()) ,
                new KeyValuePair<string, string>("IsAdmin", User.IsAdmin().ToString()),
                new KeyValuePair<string, string>("IsPublisher", User.IsPublisher().ToString()),
                new KeyValuePair<string, string>("IsSubscriber", User.IsSubscriber().ToString()),
                new KeyValuePair<string, string>("PartnerGln", User.GetContextPartner().Gln.ToString()),

            };
            return requestHeaders;
        }
    }
}
