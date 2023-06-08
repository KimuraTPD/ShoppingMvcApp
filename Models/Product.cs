using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingApp.Models
{
    public class Product
    {
        // 商品ID
        [Key]
        public int productId { get; set; }
        public string productName { get; set; }
        public int price { get; set; }
        public string create_date { get; set; }
        public string image_url { get; set; }
    }
}