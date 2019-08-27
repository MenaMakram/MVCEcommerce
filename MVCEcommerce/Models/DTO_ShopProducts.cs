using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCEcommerce.Models
{
    public class DTO_ShopProducts
    {
        public virtual List<Product> userproduct { get; set; }
        public Product userprd { get; set; }
    }
}