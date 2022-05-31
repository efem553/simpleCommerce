using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleCommerce_Models.ViewModels
{

    public class CheckoutVM
    {
        public Order Order { get; set; }
        public IEnumerable<SelectListItem>? ProvinceSelectList { get; set; }
        public CheckoutTotalVM? CheckoutTotalVM { get; set; }
    }
}
