using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel.DataAnnotations;

namespace simpleCommerce_Models
{
    
    public class Picture
    {
        
        public Guid PictureId { get; set; }
        [Required(ErrorMessage ="Picture cant be null")]
        public byte[]? ImageData { get; set; }
    }
}
