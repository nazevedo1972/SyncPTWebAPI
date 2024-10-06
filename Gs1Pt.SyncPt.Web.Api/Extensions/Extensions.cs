using Azure;
using Gs1Pt.SyncPt.Domain.Entities.RestApi.V1.TradeItems;
using Gs1Pt.SyncPt.Domain.Entities.RestApi.V1.TradeItems.Errors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace Gs1Pt.SyncPt.Web.Api.Extensions
{
    public static class Extensions
    {
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection != null && collection.Any() && collection.FirstOrDefault() != null;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }

        public static int Size<T>(this IEnumerable<T> collection)
        {
            return collection != null ? collection.Count() : 0;
        }

        public static T[] Add<T>(this T[] arr, T itemToAdd)
        {
            return (arr ?? Enumerable.Empty<T>()).Concat(new[] { itemToAdd }).ToArray();
        }

        public static bool TryParseJson<T>(this string @this, out T result)
        {
            bool success = true;
            var settings = new JsonSerializerSettings
            {
                Error = (sender, args) => { success = false; args.ErrorContext.Handled = true; },
                MissingMemberHandling = MissingMemberHandling.Error
            };
            result = JsonConvert.DeserializeObject<T>(@this, settings);
            return success;
        }

        public static string DescriptionAttr<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }
        public static T GetEnumValue<T>(int intValue) where T : struct, IConvertible
        {
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }

            return (T)Enum.ToObject(enumType, intValue);
        }

        public static double ConvertToDouble(this string value)
        {
            if (value.Contains(","))
            {
                return Convert.ToDouble(value);
            }
            return Convert.ToDouble(value, CultureInfo.InvariantCulture);
        }

        public static int? ConvertToInt(this string value)
        {
            if (int.TryParse(value, out int intValue))
            {
                return intValue;
            }
            return null;
        }

        public static string GetValueOrNull(this string valueAsString)
        {
            if (string.IsNullOrEmpty(valueAsString))
            {
                return null;
            }

            return valueAsString.Trim();
        }

        public static T? GetValueOrNull<T>(this string valueAsString) where T : struct
        {
            if (string.IsNullOrEmpty(valueAsString))
            {
                return null;
            }
            return (T)Convert.ChangeType(valueAsString, typeof(T));
        }
        public static string ReturnGtinValidOrVariableGtin(string gtin)
        {
            try
            {
                if (gtin != null && gtin != "" && gtin.Length > 2)
                {
                    if (gtin.Substring(0, 2) == "26" || gtin.Substring(0, 2) == "29" || gtin.Substring(0, 2) == "27" || gtin.Substring(0, 2) == "28")
                    {
                        return gtin;
                    }
                    else
                    {
                        if (gtin.Length < 14) { gtin = gtin.PadLeft(14, '0'); }
                    }
                }
                return gtin;
            }
            catch
            {
                return gtin;
            }

        }

        public static bool Valid(this TradeItemCountry tradeItemCountry)
        {
            return tradeItemCountry != null &&
                    !string.IsNullOrEmpty(tradeItemCountry.Code);
        }

        public static bool Valid(this TradeItemCodeList tradeItemCodeList)
        {
            return tradeItemCodeList != null &&
                   (!string.IsNullOrWhiteSpace(tradeItemCodeList.CodeListValue) ||
                   !string.IsNullOrWhiteSpace(tradeItemCodeList.CodeListValue31));
        }

        public static bool Valid(this IEnumerable<TradeItemCodeList> tradeItemCodeLists)
        {
            return tradeItemCodeLists.IsNotNullOrEmpty() &&
                    tradeItemCodeLists.Any(x => x.Valid());
        }

        public static bool Valid(this TradeItemMeasurement tradeItemMeaserument)
        {
            return tradeItemMeaserument != null &&
                    tradeItemMeaserument.Value.GetValueOrDefault(0) > 0 &&
                    !string.IsNullOrWhiteSpace(tradeItemMeaserument.UnitOfMeasure?.CodeListValue31);
        }

        public static bool Valid(this TradeItemPrice tradeItemPrice)
        {
            return tradeItemPrice != null &&
                    tradeItemPrice.Price.GetValueOrDefault(0) > 0 &&
                    !string.IsNullOrWhiteSpace(tradeItemPrice.Currency?.Code);
        }

        public static bool Valid(this TradeItemCurrency tradeItemCurrency)
        {
            return tradeItemCurrency != null &&
                    !string.IsNullOrWhiteSpace(tradeItemCurrency.Code);
        }

        public static bool Valid(this TradeItemMultiLanguage tradeItemMultiLanguage)
        {
            return tradeItemMultiLanguage != null &&
                    !string.IsNullOrWhiteSpace(tradeItemMultiLanguage.Value) &&
                    !string.IsNullOrWhiteSpace(tradeItemMultiLanguage.LanguageCode);
        }

        public static bool Valid(this IEnumerable<TradeItemMultiLanguage> tradeItemMultiLanguages)
        {
            return tradeItemMultiLanguages.IsNotNullOrEmpty() &&
                    tradeItemMultiLanguages.Any(x => x.Valid());
        }

        public static bool Valid(this IEnumerable<TradeItemAvp> tradeItemAvps)
        {
            return tradeItemAvps.IsNotNullOrEmpty() && tradeItemAvps.Any(x => x != null &&
                                                                                !string.IsNullOrEmpty(x.Name) &&
                                                                                !string.IsNullOrEmpty(x.Value));
        }

        public static IEnumerable<TradeItemMultiLanguage> GetValid(this IEnumerable<TradeItemMultiLanguage> tradeItemMultiLanguages)
        {
            return tradeItemMultiLanguages?.Where(x => x.Valid());
        }

        public static IEnumerable<TradeItemCodeList> GetValid(this IEnumerable<TradeItemCodeList> tradeItemCodeLists)
        {
            var validEntities = tradeItemCodeLists.Where(x => x.Valid());
            foreach (var validEntity in validEntities)
            {
                validEntity.CodeListValue = validEntity.CodeListValue31;
                validEntity.CodeListValue31 = null;
                validEntity.CodeListValue28 = null;
            }
            return validEntities;
        }

        public static IEnumerable<TradeItemAvp> GetValid(this IEnumerable<TradeItemAvp> tradeItemAvps)
        {
            return tradeItemAvps.Where(x => x != null &&
                                                    !string.IsNullOrEmpty(x.Name) &&
                                                    !string.IsNullOrEmpty(x.Value));
        }
    }

    

}
