using simpleCommerce_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace simpleCommerce_Utility
{
    public static class JsonObjects
    {
        public class PropertyJSON
        {
            public Guid propertyId { get; set; }
            public string? name { get; set; }
            public string? value { get; set; }
            public Guid? button { get; set; }
        }
        public class TagJSON
        {
            public Guid tagId { get; set; }
            public string? name { get; set; }
            public Guid? button { get; set; }
        }
        public class CartItemJSON
        {
            public Guid cartItemId { get; set; }
            public int quantity { get; set; }
        }

        public class ProvinceJSON
        {
            public string Id { get; set; }
            public String Province { get; set; }
        }


        public static IList<ProductProperty> ConvertToProductProperty (Guid productId,string propertyJSON)
        {
            IList<ProductProperty> productProperties = new List<ProductProperty>();
            if (productId != Guid.Empty || !string.IsNullOrEmpty(propertyJSON))
            { 
                var serializedProductProperty = JsonSerializer.Deserialize<List<PropertyJSON>>(propertyJSON);
                foreach (var item in serializedProductProperty)
                {
                    productProperties.Add(new ProductProperty
                    {
                        ProductId = productId,
                        PropertyId = item.propertyId,
                        Value = item.value,
                    });

                }
            }
            return productProperties ;
        }

        public static IList<ProductTag> ConvertToProductTag(Guid productId, string tagJSON)
        {
            IList<ProductTag> productTags = new List<ProductTag>();
            if (productId != Guid.Empty || !string.IsNullOrEmpty(tagJSON))
            {
                var serializedProductTag = JsonSerializer.Deserialize<List<TagJSON>>(tagJSON);
                foreach (var item in serializedProductTag)
                {
                    productTags.Add(new ProductTag
                    {
                        ProductId = productId,
                        TagId = item.tagId,
                    });

                }
            }
            return productTags;
        }

        public static List<CartItemJSON> ConvertToCartItem(string cartItemsJSON)
        {
            List<CartItemJSON> cartItems = new List<CartItemJSON>();
            if (!String.IsNullOrEmpty(cartItemsJSON))
            {
                var serializedCartItems = JsonSerializer.Deserialize<List<CartItemJSON>>(cartItemsJSON);
                if (serializedCartItems!=null)
                {                
                    foreach (var item in serializedCartItems)
                    {
                        cartItems.Add(new CartItemJSON
                        {
                            cartItemId=item.cartItemId,
                            quantity=item.quantity
                        });

                    }
                }
            }
            return cartItems;
        }

        public static List<Province> ConvertToProvinceModel(string provinceJSON)
        {
            List<Province> provinces= new List<Province>();
            if (!String.IsNullOrEmpty(provinceJSON))
            {
                var serializedCartItems = JsonSerializer.Deserialize<List<Province>>(provinceJSON);
                if (serializedCartItems != null)
                {
                    foreach (var item in serializedCartItems)
                    {
                        provinces.Add(new Province
                        {
                            Id = item.Id,
                            Name = item.Name
                        });

                    }
                }
            }
            return provinces;
        }
        
    }
}
