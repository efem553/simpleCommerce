using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleCommerce_Models
{
    public class Cart
    {
        [Key]
        public Guid CartId { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }
    }
}
