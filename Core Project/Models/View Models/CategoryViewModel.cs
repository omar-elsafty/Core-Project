using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_Project.Models
{
    public class CategoryViewModel
    {
        public Catigory catigory { get; set; }

        public Product Product { get; set; }
        public List<Catigory> Catigories { get; set; }

        public List<PaymentType> PaymentTypes { get; set; }
        public List<IFormFile> ProductImage { get; set; }

    }
}
