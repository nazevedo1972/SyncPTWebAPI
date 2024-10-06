
using Gs1Pt.SyncPt.Domain.Entities.RestApi.V1.TradeItems.Errors;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace Gs1Pt.SyncPt.Api.Models.Dto_s.Subscriptions
{
  
    public class TradeItemSubscription
    {
        public int SubcriberPartnerId { get; set; }
        public int SubcribedPartnerId { get; set; }
        public string GTIN { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }

    public class SubscriptionReturn
    {
        public string Message { get; set; }
        public int SubscriptionId { get; set; }
    }

    public class TradeItemSubscriptionsAPI
    {
        public string SubcriberPartnerGLN { get; set; }
        public string GTIN { get; set; }
        public bool Inactivate { get; set; }

    }
}



namespace Gs1Pt.SyncPt.Api.Models.Dto_s.TradeItems
{

    public class UpsertTradeItemResponse : Response
    {
        public int? TradeItemVersionId { get; set; }
    }
    public class ValidationResponse
    {
        public Response Response { get; set; }
        public TradeItemValidation TradeItemValidation { get; set; }
    }
    public class Response
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class TradeItemMessage
    {
        public string Message { get; set; }

    }
    public class TradeItemValidation
    {
        public TradeItemValidationType Type { get; set; }

        //[JsonIgnore]
        public TradeItemError TradeItemError { get; set; }
    }

    public class TradeItemReturn
    {
        public string Message { get; set; }
        public int TradeItemId { get; set; }
    }

    public enum TradeItemValidationType
    {
        WARNING,
        ERROR
    }


}