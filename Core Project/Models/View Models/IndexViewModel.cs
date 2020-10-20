using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_Project.Models
{
    public class IndexViewModel
    {
        public List<Product> Products { get; set; }
        public List<Catigory> Catigories { get; set; }
        public List<Tag> Tags { get; set; }

    }
}
