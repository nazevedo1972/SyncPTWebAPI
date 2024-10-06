using Gs1Pt.SyncPt.Api.Models.Dto_s.TradeItems;
using Gs1Pt.SyncPt.Domain.Entities.RestApi.V1.CodeLists;
using Gs1Pt.SyncPt.Domain.Entities.RestApi.V1.TradeItems;
using Gs1Pt.SyncPt.Web.Api.Extensions;
using Gs1Pt.SyncPt.Web.Api.HttpClients;
using Gs1Pt.SyncPt.Web.Api.Models.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;


namespace Gs1Pt.SyncPt.Web.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("3.0")]
    [Route("api/v3/[controller]")]
    public class Gs1GlobalRegistryController : ControllerBase
    {
        private readonly ISyncPtInternalApi _syncPtInternalApi;
        private readonly IMemoryCache _memoryCache;
        private const string APIPREFIX = "api/v1/Gs1GlobalRegistry";

        public Gs1GlobalRegistryController(ISyncPtInternalApi syncPtInternalApi)
        {
            _syncPtInternalApi = syncPtInternalApi;
        }


        [HttpPost("Search", Name = "GetFromRegistry")]
        public async Task<dynamic> GetFromRegistry(string[] gtins)
        {
            var httpRequestHeaderInfo = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("PartnerId", User.GetContextPartner().Id.ToString()),
                new KeyValuePair<string, string>("PartnerGln", User.GetContextPartner().Gln.ToString()),
                new KeyValuePair<string, string>("Username", User.GetUsername()) ,
                new KeyValuePair<string, string>("IsAdmin", User.IsAdmin().ToString()),
                new KeyValuePair<string, string>("CreatedOn", DateTime.UtcNow.ToString()),
                new KeyValuePair<string, string>("CreatedBy", User.Identity.Name)
            };

            var result = await _syncPtInternalApi.PostAsync<dynamic, List<Gs1GlobalRegistryTradeItemViewModel>>($"{APIPREFIX}/api/gtins", gtins, httpRequestHeaderInfo);
            return result;
        }

    }


    public class Gs1GlobalRegistryTradeItemViewModel
    {
        public bool? Success { get; set; }
        public bool? Available { get; set; }
        public bool Complete { get; set; }
        public string DateCreated { get; set; }
        public string DateUpdated { get; set; }
        public string CompanyName { get; set; }
        public string MoName { get; set; }
        public string Gtin { get; set; }
        public string BrandName { get; set; }
        public string GpcCode { get; set; }
        public string NetContent { get; set; }

        public string License { get; set; }
        public string NetContentUom { get; set; }
        public string Status { get; set; }
        public string TargetMarket { get; set; }
        public string TradeItemDescription { get; set; }
        public string TradeItemImageUrl { get; set; }

        public CountryOfSaleCode[] countryOfSaleCode { get; set; }

        public TradeItemDescription[] productDescription { get; set; }

        public string Code { get; set; }
        public string Message { get; set; }
        public string AdditionalMessage { get; set; }
    }
    public class TradeItemDescription
    {
        public string language { get; set; }
        public string value { get; set; }
    }

    public class CountryOfSaleCode
    {
        public string numeric { get; set; }
        public string alpha2 { get; set; }
        public string alpha3 { get; set; }
    }
}
