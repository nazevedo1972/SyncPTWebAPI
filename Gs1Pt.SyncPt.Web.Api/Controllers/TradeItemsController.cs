using Gs1Pt.SyncPt.Api.Models.Dto_s.Subscriptions;
using Gs1Pt.SyncPt.Api.Models.Dto_s.TradeItems;
using Gs1Pt.SyncPt.Component.Auth.Models.Identity;
using Gs1Pt.SyncPt.Component.Auth.Services;
using Gs1Pt.SyncPt.Domain.Entities.RestApi.V1.CodeLists;
using Gs1Pt.SyncPt.Domain.Entities.RestApi.V1.Filters;
using Gs1Pt.SyncPt.Domain.Entities.RestApi.V1.TradeItems;
using Gs1Pt.SyncPt.Web.Api.Extensions;
using Gs1Pt.SyncPt.Web.Api.HttpClients;
using Gs1Pt.SyncPt.Web.Api.Models.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Reflection;
using System.Text;

namespace Gs1Pt.SyncPt.Web.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("3.0")]
    [Route("api/v3/[controller]")]
    public class TradeItemsController : ControllerBase
    {
        private readonly ILogger<TradeItemsController> _logger;
        private readonly UserManager<AuthUser> _userManager;
        private readonly IAuthService _authService;
        private readonly ISyncPtInternalApi _syncPtInternalApi;
        private readonly IMemoryCache _memoryCache;
        private const string APIPREFIX = "api/v1/TradeItems";
        public TradeItemsController(ILogger<TradeItemsController> logger,
                                    UserManager<AuthUser> userManager,
                                    ISyncPtInternalApi syncPtInternalApi,
                                    IAuthService authService)
        {
            _logger = logger;
            _userManager = userManager;
            _authService = authService;
            _syncPtInternalApi = syncPtInternalApi;
        }

        [HttpPost("search", Name = "SearchAsync")]
        public async Task<TradeItemSearch> SearchAsync(TradeItemSearchFilter tradeItemFilter)
        {
            var tmp = await _syncPtInternalApi.PostAsync<TradeItemSearchFilter, TradeItemSearch>($"{APIPREFIX}/api/search", tradeItemFilter, SetRequestHeaders());
            return tmp;
        }

        [HttpGet("", Name = "GetLastVersionByGtinGlnAsync")]
        [Produces("application/json")]
        public async Task<dynamic?> GetLastVersionByGtinGlnAsync(string gln, string gtin)
        {
            try
            {
                var result = await _syncPtInternalApi.GetAsync<TradeItem>($"{APIPREFIX}/api/getbygtingln/{gln}/{gtin}", SetRequestHeaders());

                if (result == null)
                {
                    // return NotFound();
                    TradeItemReturn tmp = new TradeItemReturn();
                    tmp.Message = "Item not found";
                    tmp.TradeItemId = 0;
                    return Created("", tmp);
                }
                return result;
            }
            catch (Exception e)
            {
                TradeItemReturn tmp = new TradeItemReturn();
                tmp.Message = "Service Unavailable";
                tmp.TradeItemId = 0;
                return Created("", tmp);
            }
        }

        [HttpGet("GetByGtin", Name = "GetLastVersionByGtinAsync")]
        [Produces("application/json")]
        public async Task<dynamic?> GetLastVersionByGtinAsync(string gtin)
        {
            try
            {
                var result = await _syncPtInternalApi.GetAsync<TradeItem>($"{APIPREFIX}/api/getbygtin/{gtin}", SetRequestHeaders());

                if (result == null)
                {
                    // return NotFound();
                    TradeItemReturn tmp = new TradeItemReturn();
                    tmp.Message = "Item not found";
                    tmp.TradeItemId = 0;
                    return Created("", tmp);
                }
                return result;
            }
            catch (Exception e)
            {
                TradeItemReturn tmp = new TradeItemReturn();
                tmp.Message = "Service Unavailable";
                tmp.TradeItemId = 0;
                return Created("", tmp);
            }
        }

        [HttpPost("Upsert", Name = "UpsertAsync")]
        public async Task<dynamic> UpsertAsync(TradeItem tradeItem)
        {
            try
            {
                var result = await _syncPtInternalApi.PostAsync<TradeItem, UpsertTradeItemResponse>($"{APIPREFIX}/api/upsert", tradeItem, SetRequestHeaders());
                if (result == null)
                {
                    // return NotFound();
                    TradeItemReturn tmp = new TradeItemReturn();
                    tmp.Message = "Item not created";
                    tmp.TradeItemId = 0;
                    return Created("", tmp);
                }
                return tradeItem;
            }
            catch (Exception e)
            {
                TradeItemReturn tmp = new TradeItemReturn();
                tmp.Message = "Service Unavailable";
                tmp.TradeItemId = 0;
                return Created("", tmp);
            }
        }

        [HttpPost("PublishToGdsn", Name = "PublishToGdsnAsync")]
        public async Task<string> PublishToGdsnAsync(TradeItemMessage tradeItemmessage)
        {
            try
            {
                string message = tradeItemmessage.Message.ToString();

                string URL = "https://syncpt-datapool-wa-api-prd.azurewebsites.net/api/InsertIntoDirectlyIntoSB";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                client.BaseAddress = new Uri(URL);
                request.Method = "POST";

                // Add an Accept header for JSON format.

                request.ContentType = "application/json";

                string data = "{\"data\":\"" + message + "\"}";

                // HttpContent content = new StringContent(Base64Encode(message), Encoding.UTF8, "application/json");

                System.Net.Http.HttpContent content = new StringContent(data, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage messge = client.PostAsync(URL, content).Result;
                string description = string.Empty;
                if (messge.IsSuccessStatusCode)
                {
                    string result = messge.Content.ReadAsStringAsync().Result;
                    description = result;
                    return "Successfully sent to Datapool";
                }
                else
                {
                    return "Error Sending to Datapool";

                }
            }
            catch (Exception e)
            {
                return "Service Unavailable";
            }
        }

        [HttpPut("Publish", Name = "PublishAsync")]
        public async Task<dynamic> PublishAsync(string gln, string gtin)
        {
            try
            {
                var result = await _syncPtInternalApi.PutAsync<int, ValidationResponse>($"{APIPREFIX}/api/{gln}/{gtin}/publish", 0, SetRequestHeaders());
                return result;
            }
            catch (Exception e)
            {
                TradeItemReturn tmp = new TradeItemReturn();
                tmp.Message = "Service Unavailable";
                tmp.TradeItemId = 0;
                return Created("", tmp);
            }
        }

        [HttpPut("Inactive", Name = "InactiveAsync")]
        public async Task<dynamic> InactiveAsync(string gln, string gtin)
        {
            try
            {
                var result = await _syncPtInternalApi.PutAsync<int, UpsertTradeItemResponse>($"{APIPREFIX}/api/{gln}/{gtin}/inactive", 0, SetRequestHeaders());
                return result;
            }
            catch (Exception e)
            {
                TradeItemReturn tmp = new TradeItemReturn();
                tmp.Message = "Service Unavailable";
                tmp.TradeItemId = 0;
                return Created("", tmp);
            }
        }

        [HttpPut("Active", Name = "ActiveAsync")]
        public async Task<dynamic> ActiveAsync(string gln, string gtin)
        {
            try
            {
                var result = await _syncPtInternalApi.PutAsync<int, UpsertTradeItemResponse>($"{APIPREFIX}/api/{gln}/{gtin}/active", 0, SetRequestHeaders());
                return result;
            }
            catch (Exception e)
            {
                TradeItemReturn tmp = new TradeItemReturn();
                tmp.Message = "Service Unavailable";
                tmp.TradeItemId = 0;
                return Created("", tmp);
            }
        }

        [HttpPut("Delete", Name = "DeleteAsync")]
        public async Task<dynamic> DeleteAsync(string gln, string gtin)
        {
            try
            {
                var result = await _syncPtInternalApi.PutAsync<int, UpsertTradeItemResponse>($"{APIPREFIX}/api/{gln}/{gtin}/delete", 0, SetRequestHeaders());
                return result;
            }
            catch (Exception e)
            {
                TradeItemReturn tmp = new TradeItemReturn();
                tmp.Message = "Service Unavailable";
                tmp.TradeItemId = 0;
                return Created("", tmp);
            }
        }

        [HttpPut("UnDelete", Name = "UnDeleteAsync")]
        public async Task<dynamic> UnDeleteAsync(string gln, string gtin)
        {
            try
            {
                var result = await _syncPtInternalApi.PutAsync<int, UpsertTradeItemResponse>($"{APIPREFIX}/api/{gln}/{gtin}/undelete", 0, SetRequestHeaders());
                return result;
            }
            catch (Exception e)
            {
                TradeItemReturn tmp = new TradeItemReturn();
                tmp.Message = "Service Unavailable";
                tmp.TradeItemId = 0;
                return Created("", tmp);
            }
        }

        [HttpPost("UpsertSubscription", Name = "UpsertSubscription")]
        public async Task<dynamic> UpsertSubscription(TradeItemSubscriptionsAPI tradeItemSubscription)
        {
            try
            {
                var result = await _syncPtInternalApi.PostAsync<TradeItemSubscriptionsAPI, SubscriptionReturn>($"{APIPREFIX}/api/subscriptions/", tradeItemSubscription, SetRequestHeaders());
                return result;
            }
            catch (Exception e)
            {
                SubscriptionReturn tmp = new SubscriptionReturn();
                tmp.Message = "Service Unavailable";
                tmp.SubscriptionId = 0;
                return Created("", tmp);
            }
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
