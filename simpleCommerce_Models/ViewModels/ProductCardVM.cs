using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleCommerce_Models.ViewModels
{
    public class ProductCardVM
    {
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? CategoryName { get; set; }
        public string? Price { get; set; }
        public string? Picture { get; set; }
    }
}
