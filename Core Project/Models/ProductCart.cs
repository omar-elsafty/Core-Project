using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core_Project.Models
{
    public class ProductCart
    {
        public int ProductId { get; set; }

        public int CartId { get; set; }

        public virtual Product Product { get; set; }

        public virtual Cart Cart { get; set; }

        public int Quantity { get; set; }

    }
}
