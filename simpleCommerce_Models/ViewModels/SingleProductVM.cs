using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleCommerce_Models.ViewModels
{
    public class SingleProductVM
    {
        public Product Product { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<ProductProperty> Properties { get; set; }

        public IEnumerable<string> Pictures { get; set; }
    }
}
