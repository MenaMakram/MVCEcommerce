using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCEcommerce.Models
{
    public class DTO_SHOP
    {
        public virtual List<Product> Products { get; set; } = new List<Product>();
        public virtual List<Category> Categoeris { get; set; } = new List<Category>();

    }
}