using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleCommerce_Models.ViewModels
{
    public class ConfirmationVM
    {
        public Order Order { get; set; }
        public CheckoutTotalVM? CheckoutTotalVM { get; set; }
    }
}
