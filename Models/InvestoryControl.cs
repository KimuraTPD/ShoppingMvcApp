using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingMvcApp.Models
{
    [Serializable]
    public class InvestoryControll
    {
        // 購入履歴ID
        [Key]
        [Display(Name="商品ID")]
        public int productId { get; set; }
        [Display(Name="在庫数")]
        public string InvestoryAmount { get; set; }

        public InvestoryControll(){}

        public void showData()
        {
            Console.WriteLine("商品ID=" + productId + ", 在庫数=" + InvestoryAmount);
        }
    }
}