using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core_Project.Models
{
    public class PaymentType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public bool IsSelected { get; set; }

        public virtual List<Cart> Carts { get; set; }

        public virtual List<ProductPaymentType> ProductPaymentTypes { get; set; }
    }
}
