using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCEcommerce.Models
{
    public class Product
    {
        
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [ConcurrencyCheck]
        public int Quantity { get; set; }
        [DataType("date")]
        [Required]
        public DateTime ExpireDate { get; set; }
        [Required]
        public string Description { get; set; }
        [ForeignKey("user")]
        public string UserId { get; set; }
        public virtual ApplicationUser user { get; set; }
        public virtual ICollection<OrderUserProduct> orderuserproducts { get; set; } = new List<OrderUserProduct>();
        public string Images { get; set; }
        public virtual Category Category { get; set; }

    }
}