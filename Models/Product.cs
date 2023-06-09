using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingApp.Models
{
    [Serializable]
    public class Product
    {
        // 商品ID
        [Key]
        public int productId { get; set; }
        [Display(Name="商品名")]
        public string productName { get; set; }
        [Display(Name="価格")]
        public int price { get; set; }
        [Display(Name="登録日")]
        public string create_date { get; set; }
        [Display(Name="商品画像")]
        public string image_url { get; set; }
        [Display(Name="個数")]
        public int count { get; set; }

        public Product() { }

        public Product(int productId, string productName, int price)
        {
            this.productId = productId;
            this.productName = productName;
            this.price = price;
        }
        public Product(int productId, string productName, int price,int count)
        {
            this.productId = productId;
            this.productName = productName;
            this.price = price;
            this.count =count;
        }

        public void showData()
        {
            Console.WriteLine("priductId = " + productId + ", productName = " + productName + ", price = " + price +
                ", create_date = " + create_date + ", image_url" + image_url + ", count = " + count);
        }
    }
}