using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace ShoppingMvcApp.Models
{
    public class JoinTables
    {
        public Product product { get; set;}

        public PurchaseHistory purchaseHistory { get; set; }
    }
}