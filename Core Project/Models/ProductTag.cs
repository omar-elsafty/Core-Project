﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_Project.Models
{
    public class ProductTag
    {

        public int ProductId { get; set; }

        public int TagId { get; set; }

        public virtual Product Product { get; set; }

        public virtual Tag Tag { get; set; }

    }
}
