using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_Project.Models
{
    public class ProductPaymentType
    {
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int PaymentTypeId { get; set; }

        public virtual PaymentType PaymentType { get; set; }
    }
}
