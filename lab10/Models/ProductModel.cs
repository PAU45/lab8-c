using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab10.Models
{
    public class ProductModel
    {
    
        public int Id { get; set; }

       
        public string Name { get; set; }

      
        public decimal Price { get; set; }

        
        public int StockQuantity { get; set; }
    }
}