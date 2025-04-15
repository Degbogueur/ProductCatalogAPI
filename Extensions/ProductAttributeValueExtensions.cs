using Newtonsoft.Json;
using ProductCatalog.Helpers.Enums;
using ProductCatalog.Models;

namespace ProductCatalog.Extensions
{
    public static class ProductAttributeValueExtensions
    {
        public static object GetValue(this ProductAttributeValue productAttributeValue)
        {
            var value = productAttributeValue.Value;
            var type = productAttributeValue.AttributeDefinition?.Type;

            return type switch
            {
                AttributeType.Text => JsonConvert.DeserializeObject<string>(value),
                AttributeType.Number => JsonConvert.DeserializeObject<decimal>(value),
                AttributeType.Date => JsonConvert.DeserializeObject<DateTime>(value),
                AttributeType.Boolean => JsonConvert.DeserializeObject<bool>(value),
                AttributeType.File => JsonConvert.DeserializeObject<IFormFile>(value),
                _ => value
            };
        }
    }
}
