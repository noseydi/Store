﻿using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : BaseAuditableEntity , ICommands
    {
        public string Title { get; set;  }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string Description { get; set ; }
        public bool IsActive { get  ; set  ; }
        
        public string Summary { get  ; set  ; }

        //relation
        public int ProductTypeId { get; set; }
        public int ProductBrandId { get; set; }
        public  ProductBrand ProductBrand { get; set; }
        public ProductType ProductType { get; set; }


       
     
    }
}
