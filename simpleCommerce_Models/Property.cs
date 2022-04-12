using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleCommerce_Models
{
    public class Property
    {
        public Guid PropertyId { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }

    }
}
