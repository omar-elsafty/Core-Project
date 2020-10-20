using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core_Project.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public DateTime When { get; set; }

        public virtual PaymentType PaymentType { get; set; }

        [ForeignKey("PaymentType")]
        public int FK_PaymentTypeId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [ForeignKey("User")]
        public string FK_BuyerId { get; set; }

        public virtual List<ProductCart> ProductCarts { get; set; }
    }
}
