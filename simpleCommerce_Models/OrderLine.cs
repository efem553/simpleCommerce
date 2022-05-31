using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleCommerce_Models
{
    public class OrderLine
    {
       [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LineNo { get; set; }
        [Required]
        public Guid OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        public string ProductName { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
