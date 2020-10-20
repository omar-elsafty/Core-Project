using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core_Project.Models
{
    public class Image
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        //[Column(TypeName = "image")]
        [NotMapped]
        [DisplayName("Upload Photo")]
        public IFormFile ProductImage { get; set; }

        public Product Product { get; set; }

        [ForeignKey("Product")]

        public int? FK_ProductId { get; set; }

    }
}
