using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingMvcApp.Models
{
    public class User
    {
        // ユーザーID
        [Key]
        public int userId { get; set; }
        public string name { get; set; }
        public string mail { get; set; }
        public string password { get; set; }
        public string tel { get; set; }
        public string address { get; set; }
        // public List<Product> buyList { get; set; }

        public User(){

        }

        public User(int userID, string userName, string mail, string password , string tel, string address)
        {
            this.userId = userID;
            this.name = userName;
            this.mail = mail;
            this.password = password;
            this.tel =  tel;
            this.address = address;
        }
    }
}