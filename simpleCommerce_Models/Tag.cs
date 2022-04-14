using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleCommerce_Models
{
    public class Tag
    {
        public Guid TagId { get; set; }
        public string Name { get; set; } = default!;
        public string FilterName { get; set; } = default!;
    }
}
