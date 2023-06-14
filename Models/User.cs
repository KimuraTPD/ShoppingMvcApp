using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using ShoppingMvcApp.Models;

namespace ShoppingMvcApp.Models
{
    [Serializable]
    public  class User
    {
          // ユーザーID
        [Key]
        public int userId { get; set; }
        public string name { get; set; }
        public string mail { get; set; }
        public string password { get; set; }
        public string tel { get; set; }
        public string address { get; set; }
        [Display(Name="購入履歴")]
        public ICollection<PurchaseHistory> purchaseHistorys { get; set;}

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
        public User(string userName, string mail, string password , string tel, string address)
        {
            this.name = userName;
            this.mail = mail;
            this.password = password;
            this.tel =  tel;
            this.address = address;
        }

        public User(string mail,string password){
            this.mail = mail;
            this.password = password;
        }

        internal byte[] ObjectToBytes(Object ob)
            {
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream ms = new MemoryStream();
                bf.Serialize(ms, ob);
                return ms.ToArray();
            }


            // convert byte[] to object.
        internal Object ct(byte[] arr)
            {
                MemoryStream ms = new MemoryStream();
                BinaryFormatter bf = new BinaryFormatter();
                ms.Write(arr, 0, arr.Length);
                ms.Seek(0, SeekOrigin.Begin);
                return (Object)bf.Deserialize(ms);
            }
    }
}