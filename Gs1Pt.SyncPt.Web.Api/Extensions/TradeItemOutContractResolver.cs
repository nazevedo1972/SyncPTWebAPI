using Gs1Pt.SyncPt.Domain.Entities.RestApi.V1.TradeItems;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Reflection;

namespace Gs1Pt.SyncPt.Web.Api.Extensions
{
    public class TradeItemOutContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (property.PropertyType == typeof(List<TradeItemMultiLanguage>))
            {
                property.ShouldSerialize =
                    instance =>
                    {
                        var tradeItemMultiLanguage = (List<TradeItemMultiLanguage>)property.ValueProvider.GetValue(instance);

                        if (tradeItemMultiLanguage.Valid())
                        {
                            property.ValueProvider.SetValue(instance, tradeItemMultiLanguage.GetValid().ToList());
                        }
                        else if (tradeItemMultiLanguage != null && property.PropertyName == "nutritionalClaims")
                        {
                            property.ValueProvider.SetValue(instance, tradeItemMultiLanguage.ToList());
                            return true;
                        }

                        return tradeItemMultiLanguage.Valid();
                    };
            }

            else if (property.PropertyType == typeof(List<TradeItemCodeList>))
            {
                property.ShouldSerialize =
                    instance =>
                    {
                        var currentValue = (List<TradeItemCodeList>)property.ValueProvider.GetValue(instance);

                        if (currentValue.Valid())
                        {
                            property.ValueProvider.SetValue(instance, currentValue.GetValid().ToList());
                        }

                        return currentValue.Valid();
                    };
            }

            else if (property.PropertyType == typeof(List<TradeItemAvp>))
            {
                property.ShouldSerialize =
                    instance =>
                    {
                        var tradeItemAvp = (List<TradeItemAvp>)property.ValueProvider.GetValue(instance);

                        if (tradeItemAvp.Valid())
                        {
                            property.ValueProvider.SetValue(instance, tradeItemAvp.GetValid().ToList());
                        }

                        return tradeItemAvp.Valid();
                    };
            }

            else if (property.PropertyType == typeof(TradeItemMeasurement))
            {
                property.ShouldSerialize =
                    instance =>
                    {
                        var currentValue = (TradeItemMeasurement)property.ValueProvider.GetValue(instance);
                        return currentValue.Valid();
                    };
            }

            else if (property.PropertyType == typeof(TradeItemCodeList))
            {
                property.ShouldSerialize =
                    instance =>
                    {
                        var currentValue = (TradeItemCodeList)property.ValueProvider.GetValue(instance);

                        if (currentValue.Valid())
                        {
                            currentValue.CodeListValue = currentValue.CodeListValue31;
                            currentValue.CodeListValue28 = null;
                            currentValue.CodeListValue31 = null;
                            property.ValueProvider.SetValue(instance, currentValue);
                        }

                        return currentValue.Valid();
                    };
            }

            else if (property.PropertyType == typeof(TradeItemPrice))
            {
                property.ShouldSerialize =
                    instance =>
                    {
                        var currentValue = (TradeItemPrice)property.ValueProvider.GetValue(instance);
                        return currentValue.Valid();
                    };
            }

            else if (property.PropertyType == typeof(TradeItemCurrency))
            {
                property.ShouldSerialize =
                    instance =>
                    {
                        var currentValue = (TradeItemCurrency)property.ValueProvider.GetValue(instance);
                        return currentValue.Valid();
                    };
            }

            return property;
        }

        protected override IList<JsonProperty> CreateProperties(System.Type type, MemberSerialization memberSerialization)
        {
            return base.CreateProperties(type, memberSerialization)
            .ToList();
        }


    }
}
