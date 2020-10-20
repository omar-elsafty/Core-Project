using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core_Project.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public int Discount { get; set; }

        [DataType(DataType.Currency)]
        public int Price { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(256)]
        public string Discription { get; set; }


        public List<Image> Images { get; set; }

        public virtual Catigory Catigory { get; set; }

        [ForeignKey("Catigory")]
        public int? FK_CatigoryId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [ForeignKey("User")]
        public string FK_SellerId { get; set; }

        public virtual List<ProductTag> ProductTags { get; set; }

        public virtual List<ProductPaymentType> ProductPaymentTypes { get; set; }


        public virtual List<ProductCart> ProductCarts { get; set; }
    }
}
