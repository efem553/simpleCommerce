using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleCommerce_Models.ViewModels
{
    public class CheckoutLineVM
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal { get; set; }
    }
}
