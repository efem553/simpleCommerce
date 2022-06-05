using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleCommerce_Models.ViewModels
{
    public class AboutVM
    {
        public About About { get; set; }
        public IEnumerable<FacultyLogo> FacultyLogos { get; set; }
    }
}
