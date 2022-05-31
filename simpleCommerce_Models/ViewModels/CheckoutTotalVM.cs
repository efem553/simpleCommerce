using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleCommerce_Models.ViewModels
{
    public class CheckoutTotalVM
    {
        public IEnumerable<CheckoutLineVM> Items { get; set; }
        public decimal Total { get; set; } 
    }
}
