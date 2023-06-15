using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingMvcApp.Models
{
    [Serializable]
    public class PurchaseHistory
    {
        // 購入履歴ID
        [Key]
        [Display(Name="購入履歴ID")]
        public int PurchaseHistoryId { get; set; }
        [Display(Name="明細番号")]
        public int detailsId { get; set; }
        [Display(Name="ユーザーID")]
        public int userId { get; set; }
        [Display(Name="商品ID")]
        public int productId { get; set; }
        [Display(Name="購入数")]
        public int count { get; set; }
        [Display(Name="購入日時")]
        public string purchaseDate { get; set; }

        public User user { get; set; }
        public Product product { get; set; }

        public PurchaseHistory(){}

        public void showData()
        {
            Console.WriteLine("購入履歴ID=" + PurchaseHistoryId + ", 明細ID=" + detailsId + ", ユーザーID=" + userId + ", 商品ID=" + productId + ", 購入数=" + count + ", 購入日時=" + purchaseDate);
        }
    }
}