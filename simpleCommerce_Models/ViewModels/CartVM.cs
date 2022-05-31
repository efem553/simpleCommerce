using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleCommerce_Models.ViewModels
{
    public class CartVM
    {
        public IEnumerable<CartItemLineVM> CartItems { get; set; }
        public decimal Subtotal { get; set; }
    }
}
