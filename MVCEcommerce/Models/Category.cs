using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCEcommerce.Models
{
    public class Category
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}