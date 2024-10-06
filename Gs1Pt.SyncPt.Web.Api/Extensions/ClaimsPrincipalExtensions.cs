using Gs1Pt.SyncPt.Component.Auth.Models.Dtos;
using Gs1Pt.SyncPt.Web.Api.Models.Constants;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Gs1Pt.SyncPt.Web.Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool UserIsLoaded(this ClaimsPrincipal user)
        {
            return user != null && user.Claims.IsNotNullOrEmpty();
        }

        public static string GetId(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(x => x.Type == "Id").Value;
        }

        public static string GetName(this ClaimsPrincipal user)
        {
            return user.Identity.Name;
        }

        public static string GetUsername(this ClaimsPrincipal user)
        {
            try
            {
                return user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            }
            catch
            {
                return "";
            }
        }


        public static bool IsPublisher(this ClaimsPrincipal user)
        {
            if (user == null)
            {
                return false;
            }
            return user.GetContextPartner()?.IsPublisher ?? false;
        }

        public static bool IsSubscriber(this ClaimsPrincipal user)
        {
            if (user == null)
            {
                return false;
            }
            var tmp = user.GetContextPartner()?.IsSubscriber;
            return tmp ?? false;
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            if (user == null)
            {
                return false;
            }

            return user.HasRole(RolesConstants.ROLE_ADMIN);
        }

        public static PartnerDto[] GetPartners(this ClaimsPrincipal user)
        {
            var userPartnersIdsStr = user.Claims.FirstOrDefault(x => x.Type == "Partners").Value;
            var tmp= JsonConvert.DeserializeObject<PartnerDto[]>(userPartnersIdsStr);
            return tmp;
        }
        public static PartnerDto GetContextPartner(this ClaimsPrincipal user)
        {

            var contextPartnerGln = user.Claims.FirstOrDefault(x => x.Type == "ContextPartnerGln")?.Value;
            var contextPartner = user.GetPartners()?.FirstOrDefault(x => x.Gln == contextPartnerGln);
            return contextPartner ?? user.GetPartners()?.FirstOrDefault();
        }

        public static bool HasRole(this ClaimsPrincipal user, string roleName)
        {
            if (user == null)
            {
                return false;
            }

            return user.GetRoles()?.Any(x => x == roleName) ?? false;
        }


        public static string GetEmail(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
        }

        public static string GetMobilePhone(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.MobilePhone)?.Value;
        }

        public static bool IsAzureAdAuth(this ClaimsPrincipal user)
        {
            if (bool.TryParse(user.Claims.FirstOrDefault(x => x.Type == "IsAzureAdAuth")?.Value, out bool isAzureAdAuth))
            {
                return isAzureAdAuth;
            }
            return false;
        }

        public static bool IsImpersonate(this ClaimsPrincipal user)
        {
            if (bool.TryParse(user.Claims.FirstOrDefault(x => x.Type == "IsImpersonating")?.Value, out bool isImpersonate))
            {
                return isImpersonate;
            }
            return false;
        }

        public static string GetOriginalUserId(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(x => x.Type == "OriginalUserId")?.Value;
        }

        public static string GetCreationDate(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(x => x.Type == "CreationDate")?.Value;
        }

        public static string GetLastLoginDate(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(x => x.Type == "LastLoginDate")?.Value;
        }

        public static bool IsFirstLogin(this ClaimsPrincipal user)
        {
            string lastLoginDate = user.Claims.FirstOrDefault(x => x.Type == "LastLoginDate")?.Value;
            return string.IsNullOrWhiteSpace(lastLoginDate) || lastLoginDate == "01/01/1900";
        }

        //public static bool IsAdmin(this ClaimsPrincipal user)
        //{
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    return user.HasRole(RolesConstants.ROLE_ADMIN);
        //}

        //public static bool HasRnc(this ClaimsPrincipal user)
        //{
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    if (user.IsAdmin())
        //    {
        //        return true;
        //    }

        //    return user.GetContextPartnerIsRnc() &&
        //           (user.HasRole(RolesConstants.ROLE_RNCWRITE) ||
        //            user.HasRole(RolesConstants.ROLE_RNCREAD) ||
        //            user.HasRole(RolesConstants.ROLE_WRITE) ||
        //            user.HasRole(RolesConstants.ROLE_READ));
        //}

        //public static bool HasRncContractAccepted(this ClaimsPrincipal user)
        //{
        //    return (bool)user.HasRnc() && (bool)user.GetContextPartner().IsRncContractAccepted;
        //}

        //public static bool HasTradeItems(this ClaimsPrincipal user)
        //{
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    if (user.IsAdmin())
        //    {
        //        return true;
        //    }

        //    return user.HasRole(RolesConstants.ROLE_RNCWRITE) ||
        //            user.HasRole(RolesConstants.ROLE_RNCREAD) ||
        //            user.HasRole(RolesConstants.ROLE_WRITE) ||
        //           user.HasRole(RolesConstants.ROLE_READ);
        //}

        //public static bool IsPublisherAndSubscriber(this ClaimsPrincipal user)
        //{
        //    return !user.IsAdmin() &&
        //            user.IsPublisher() &&
        //            user.IsSubscriber();
        //}

        //public static bool IsPublisher(this ClaimsPrincipal user)
        //{
        //    if (user == null)
        //    {
        //        return false;
        //    }
        //    return user.GetContextPartner()?.IsPublisher ?? false;
        //}

        //public static bool IsSubscriber(this ClaimsPrincipal user)
        //{
        //    if (user == null)
        //    {
        //        return false;
        //    }
        //    var tmp = user.GetContextPartner()?.IsSubscriber;
        //    return tmp ?? false;
        //}

        //public static bool HasRole(this ClaimsPrincipal user, string roleName)
        //{
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    return user.GetRoles()?.Any(x => x == roleName) ?? false;
        //}

        //public static bool IsTradeItemOwner(this ClaimsPrincipal user, string partnerGln)
        //{
        //    if (user == null || partnerGln == null)
        //    {
        //        return false;
        //    }

        //    if (user.IsAdmin())
        //    {
        //        return true;
        //    }

        //    return user.GetContextPartner()?.Gln == partnerGln;
        //}

        //public static bool IsTradeItemOwner(this ClaimsPrincipal user, int partnerId)
        //{
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    if (user.IsAdmin())
        //    {
        //        return true;
        //    }

        //    return user.GetContextPartner()?.Id == partnerId;
        //}

        //public static bool IsTradeItemSubscriber(this ClaimsPrincipal user, TradeItemViewModel tradeItem)
        //{
        //    if (user == null || tradeItem == null)
        //    {
        //        return false;
        //    }

        //    if (user.IsAdmin())
        //    {
        //        return true;
        //    }

        //    return tradeItem.IsPublic || tradeItem.Permissions.Any(x => x.Gln == user.GetContextPartner()?.Gln);
        //}

        //public static bool CanCreateRnc(this ClaimsPrincipal user, bool hasDraftTradeItems)
        //{
        //    if (user == null)
        //    {
        //        return false;
        //    }


        //    if (user.IsAdmin())
        //    {
        //        return true;
        //    }

        //    return user.HasRnc() &&
        //           user.HasRole(RolesConstants.ROLE_RNCWRITE) &&
        //           !hasDraftTradeItems;
        //}

        //public static bool CanEditRnc(this ClaimsPrincipal user,
        //                              bool hasDraftTradeItems,
        //                              string partnerGln,
        //                              bool isLastVersion,
        //                              TradeItemStatusViewModel tradeItemStatus,
        //                              DateTime creationDate)
        //{
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    if (user.IsAdmin() && isLastVersion)
        //    {
        //        return true;
        //    }

        //    bool createdInLast6Months = (DateTime.UtcNow - creationDate).TotalDays <= 90;

        //    var tmp = user.HasRnc() &&
        //           user.HasRole(RolesConstants.ROLE_RNCWRITE) &&
        //           user.IsTradeItemOwner(partnerGln) &&
        //           // !hasDraftTradeItems &&
        //           isLastVersion &&
        //           TradeItemHelper.TradeItemEditableStatus.Contains(tradeItemStatus) &&
        //           createdInLast6Months;

        //    return tmp;
        //}

        //public static bool CanCreateTradeItem(this ClaimsPrincipal user)
        //{
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    if (user.IsAdmin())
        //    {
        //        return true;
        //    }

        //    return user.HasTradeItems() &&
        //           user.HasRole(RolesConstants.ROLE_WRITE);
        //}

        //public static bool CanEditTradeItem(this ClaimsPrincipal user, TradeItemViewModel tradeItem)
        //{
        //    if (user == null || tradeItem == null)
        //    {
        //        return false;
        //    }

        //    if (tradeItem.Status == TradeItemStatusViewModel.RECEIVED)
        //    {
        //        return false;
        //    }

        //    bool isLastVersion = tradeItem.IsLastVersion;

        //    if (user.IsAdmin() && isLastVersion)
        //    {
        //        return true;
        //    }

        //    bool hastradeitem = user.HasTradeItems();
        //    bool tradeItemEditableStatus = TradeItemHelper.TradeItemEditableStatus.Contains(tradeItem.Status);
        //    bool tmp = hastradeitem &&
        //           (user.HasRole(RolesConstants.ROLE_WRITE) || user.HasRole(RolesConstants.ROLE_RNCWRITE)) &&
        //           user.IsTradeItemOwner(tradeItem.Partner.Gln) &&
        //           isLastVersion && tradeItemEditableStatus;
        //    return tmp;
        //}

        //public static bool CanEditTradeItem(this ClaimsPrincipal user, string partnerGln, bool isLastVersion, TradeItemStatusViewModel tradeItemStatus)
        //{
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    if (user.IsAdmin() && isLastVersion)
        //    {
        //        return true;
        //    }

        //    return user.HasTradeItems() &&
        //           user.HasRole(RolesConstants.ROLE_WRITE) &&
        //           user.IsTradeItemOwner(partnerGln) &&
        //           isLastVersion &&
        //           TradeItemHelper.TradeItemEditableStatus.Contains(tradeItemStatus);
        //}

        //public static bool CanEditTradeItem(this ClaimsPrincipal user, int partnerId, bool isLastVersion, TradeItemStatusViewModel tradeItemStatus)
        //{
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    if (user.IsAdmin() && isLastVersion)
        //    {
        //        return true;
        //    }

        //    return user.HasTradeItems() &&
        //           user.HasRole(RolesConstants.ROLE_WRITE) &&
        //           user.IsTradeItemOwner(partnerId) &&
        //           isLastVersion &&
        //           TradeItemHelper.TradeItemEditableStatus.Contains(tradeItemStatus);
        //}

        //public static bool CanInactivateTradeItem(this ClaimsPrincipal user, TradeItemViewModel tradeItem)
        //{
        //    bool isSingleVersion = tradeItem.AvailableVersions.Length == 1;

        //    var tmp = user.CanEditTradeItem(tradeItem) &&
        //          ((/*!isSingleVersion &&*/
        //           (tradeItem.Status != TradeItemStatusViewModel.INACTIVE && tradeItem.Status != TradeItemStatusViewModel.RECEIVED))
        //           || (isSingleVersion && tradeItem.Status == TradeItemStatusViewModel.PUBLISHED)
        //           );
        //    return tmp;
        //}

        //public static bool CanActivateTradeItem(this ClaimsPrincipal user, TradeItemViewModel tradeItem)
        //{
        //    //bool isSingleVersion = tradeItem.AvailableVersions.Length == 1;

        //    return //user.CanEditTradeItem(tradeItem) && 
        //           //     !isSingleVersion &&
        //           tradeItem.Status == TradeItemStatusViewModel.INACTIVE;
        //}

        //public static bool CanDeleteTradeItem(this ClaimsPrincipal user, TradeItemViewModel tradeItem)
        //{
        //    bool isSingleVersion = tradeItem.AvailableVersions.Length == 1;

        //    return user.CanEditTradeItem(tradeItem) &&
        //           isSingleVersion &&
        //           (tradeItem.Status != TradeItemStatusViewModel.DELETED && tradeItem.Status != TradeItemStatusViewModel.PUBLISHED && tradeItem.Status != TradeItemStatusViewModel.RECEIVED);
        //}

        //public static bool CanUndeleteTradeItem(this ClaimsPrincipal user, TradeItemViewModel tradeItem)
        //{
        //    bool isSingleVersion = tradeItem.AvailableVersions.Length == 1;

        //    return user.CanEditTradeItem(tradeItem) &&
        //           isSingleVersion &&
        //           tradeItem.Status == TradeItemStatusViewModel.DELETED;
        //}

        //public static bool CanDuplicateTradeItem(this ClaimsPrincipal user, TradeItemViewModel tradeItem)
        //{
        //    return user.CanEditTradeItem(tradeItem);
        //}

        //public static bool CanPromoteAsLastVersion(this ClaimsPrincipal user, TradeItemViewModel tradeItem)
        //{
        //    if (user == null || tradeItem == null)
        //    {
        //        return false;
        //    }

        //    bool isLastVersion = tradeItem.IsLastVersion;

        //    if (user.IsAdmin() && !isLastVersion)
        //    {
        //        return true;
        //    }

        //    return user.HasTradeItems() &&
        //           user.HasRole(RolesConstants.ROLE_WRITE) &&
        //           user.IsTradeItemOwner(tradeItem.Partner.Gln) &&
        //           !isLastVersion;
        //}

        //public static bool CanCopySpecificInfoDataToParents(this ClaimsPrincipal user, TradeItemViewModel tradeItem)
        //{
        //    return user.CanEditTradeItem(tradeItem) &&
        //           tradeItem.TradeItemUnitDescriptor?.CodeListValue31 == TradeItemConstants.TRADEITEM_UNIT_DESCRIPTOR_BASE_UNIT_OR_EACH &&
        //           tradeItem.Hierarchy.IsNotNullOrEmpty() &&
        //           tradeItem.Hierarchy.Any(x => x.ChildTradeItemVersionsIds.Any(y => y == tradeItem.Id));
        //}

        //public static bool CanGetInformationFromChild(this ClaimsPrincipal user, TradeItemViewModel tradeItem)
        //{
        //    return user.CanEditTradeItem(tradeItem) &&
        //           (tradeItem.TradeItemUnitDescriptor?.CodeListValue31 == TradeItemConstants.TRADEITEM_UNIT_DESCRIPTOR_PACK_OR_INNER_PACK || tradeItem.TradeItemUnitDescriptor?.CodeListValue31 == TradeItemConstants.TRADEITEM_UNIT_DESCRIPTOR_CASE) &&
        //           tradeItem.ChildTradeItems.IsNotNullOrEmpty();
        //}

        //public static ImportExport[] GetImportsExports(this ClaimsPrincipal user, ImportExport[] importExports)
        //{
        //    if (user.IsAdmin())
        //    {
        //        return importExports;
        //    }

        //    var userIsApel = user.GetContextPartner().IsAPEL.GetValueOrDefault(false);

        //    if (!userIsApel)
        //    {
        //        if (user.GetContextPartnerIsModalfa())
        //        {
        //            if (user.GetUsername().Contains("mofashion.com"))
        //            {
        //                if (importExports.Any(x => x.Type == ImportExportType.EXPORT))
        //                    return importExports.Where(x => x.Name.ToLower().Contains("mo "))?.ToArray();
        //                else
        //                    return importExports.Where(x => x.Name.ToLower().Contains("têxtil") && x.Name.ToLower().Contains("mo input"))?.ToArray();
        //            }
        //            else
        //                return importExports.Where(x => x.Name.ToLower().Contains("têxtil"))?.ToArray();
        //        }
        //        else
        //            return importExports.Where(x => x.Permissions.IsPublic ||
        //                                       (x.Permissions.PartnersIds.IsNullOrEmpty() || x.Permissions.PartnersIds.Any(y => y == user.GetContextPartner().Id)))?.ToArray();
        //    }
        //    else
        //    {

        //        return importExports.Where(x => x.Name.ToLower().Contains("apel"))?.ToArray();
        //    }
        //}

        //public static string[] GetContextPartnerTradeItemClassRestrictions(this ClaimsPrincipal user)
        //{
        //    if (user == null)
        //    {
        //        return null;
        //    }

        //    return user.GetContextPartner().TradeItemClassRestrictions;
        //}

        //public static bool GetContextPartnerHasTradeItemClassRestrictions(this ClaimsPrincipal user)
        //{
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    return user.GetContextPartner().TradeItemClassRestrictions.IsNotNullOrEmpty();
        //}

        //public static string GetContextPartnerTradeItemClassDefault(this ClaimsPrincipal user)
        //{
        //    if (user == null)
        //    {
        //        return null;
        //    }

        //    return user.GetContextPartner().TradeItemClassDefault;
        //}

        //public static bool GetContextPartnerIsRnc(this ClaimsPrincipal user)
        //{
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    return (bool)user.GetContextPartner().IsRnc;
        //}

        //public static bool GetContextPartnerIsApel(this ClaimsPrincipal user)
        //{
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    return user.GetContextPartner().IsAPEL.GetValueOrDefault(false);
        //}

        //public static bool GetContextPartnerIsModalfa(this ClaimsPrincipal user)
        //{
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    return user.GetContextPartner().Gln == "5600000932865";
        //}

        //public static bool GetContextPartnerIsItmp(this ClaimsPrincipal user)
        //{
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    return user.GetContextPartner().Gln == "5600065740085";
        //}

        //public static bool GetContextPartnerIsWells(this ClaimsPrincipal user)
        //{
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    return user.GetContextPartner().Gln == "5600000905845";
        //}

        //public static bool GetContextPartnerIsDelta(this ClaimsPrincipal user)
        //{
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    return user.GetContextPartner().Gln == "5600000001486";
        //}

        //public static bool GetContextPartnerHasConsumerUnit(this ClaimsPrincipal user)
        //{
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    return user.GetContextPartner().HasConsumerUnit.GetValueOrDefault(false);
        //}


        //public static bool GetContextPartnerHasLogistics(this ClaimsPrincipal user)
        //{
        //    if (user == null)
        //    {
        //        return false;
        //    }

        //    return user.GetContextPartner().HasLogistics.GetValueOrDefault(false);
        //}


        //public static PartnerViewModel GetContextPartner(this ClaimsPrincipal user)
        //{

        //    var contextPartnerGln = user.Claims.FirstOrDefault(x => x.Type == "ContextPartnerGln")?.Value;
        //    var contextPartner = user.GetPartners()?.FirstOrDefault(x => x.Gln == contextPartnerGln);
        //    return contextPartner ?? user.GetPartners()?.FirstOrDefault();
        //}

        //public static Partner[] GetPartners(this ClaimsPrincipal user)
        //{
        //    var userPartnersIdsStr = user.Claims.FirstOrDefault(x => x.Type == "Partners").Value;
        //    return JsonConvert.DeserializeObject<PartnerViewModel[]>(userPartnersIdsStr);
        //}

        public static string[] GetRoles(this ClaimsPrincipal user)
        {
            var userPartnersIdsStr = user.Claims?.FirstOrDefault(x => x.Type == "Roles")?.Value;

            if (string.IsNullOrWhiteSpace(userPartnersIdsStr))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<string[]>(userPartnersIdsStr);
        }

        public static string GetAllClaims(this ClaimsPrincipal user)
        {
            return string.Join(";", user.Claims.Select(x => $"[{x.Type}] {x.Value}"));
        }
    }
}
