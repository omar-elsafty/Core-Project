using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core_Project.Models
{
    public class ApplicationUser:IdentityUser
    {
      

        
        [MaxLength(50)]
        public string Name { get; set; }



        [DataType(DataType.MultilineText)]
        [MaxLength(256)]
        public string Address { get; set; }

      

        public virtual List<Cart> Carts { get; set; }

    }
}
