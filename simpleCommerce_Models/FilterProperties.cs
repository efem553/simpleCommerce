using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleCommerce_Models
{
    [NotMapped]
    public class FilterProperties
    {
        public Guid CategoryId { get; set; }
        public Guid TagId { get; set; }
        public int OrderNo { get; set; }
        public decimal LowestPrice { get; set; }
        public decimal HighestPrice { get; set; }
        public string SearchText { get; set; }
    }
}
