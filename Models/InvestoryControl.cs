using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingMvcApp.Models
{
    [Serializable]
    public class InvestoryControl
    {
        // 購入履歴ID
        [Key]
        [Display(Name="商品ID")]
        public int productId { get; set; }
        [Display(Name="在庫数")]
        public int InvestoryAmount { get; set; }

        public InvestoryControl(){}

        public void showData()
        {
            Console.WriteLine("商品ID=" + productId + ", 在庫数=" + InvestoryAmount);
        }
    }
}