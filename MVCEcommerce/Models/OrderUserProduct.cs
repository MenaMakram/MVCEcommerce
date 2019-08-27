using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCEcommerce.Models
{
    public class OrderUserProduct
    {
        public int ID { get; set; }
        [ForeignKey("order")]
        public int ordeID { get; set; }
        [ForeignKey("Product")]
        public int Product_ID { get; set; }
        [Required]
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
        public virtual Orders order { get; set; }
    }
}