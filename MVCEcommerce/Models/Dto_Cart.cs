using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCEcommerce.Models
{
    public class Dto_Cart
    {
        public virtual Product user { get; set; }
        public virtual int Quantity { get; set; }
    }
}