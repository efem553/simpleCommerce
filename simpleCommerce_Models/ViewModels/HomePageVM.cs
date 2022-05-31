using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleCommerce_Models.ViewModels
{
    public class HomePageVM
    {
        public HomePageVM()
        {
            FilterProperties= new FilterProperties();
        }
        public IEnumerable<ProductCardVM> Products { get; set; }

        public FilterProperties FilterProperties { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Tag> Tags{ get; set; }

    }
}
