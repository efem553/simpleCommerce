using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleCommerce_Models
{
    public class Image
    {
        public Guid ImageId { get; set; }
        public string? Base64 { get; set; }
    }
}
