using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleCommerce_Models
{
    public class CartItem
    {

        public Guid CartItemId { get; set; }
        public Guid CartId { get; set; }
        [ForeignKey("CartId")]
        public Cart Cart { get; set; }

        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int Count { get; set; }


    }
}
