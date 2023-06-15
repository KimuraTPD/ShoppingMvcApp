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

using System.Text.RegularExpressions;

namespace ShoppingMvcApp.Models
{
    [Serializable]
    public  class User :IValidatableObject
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

        public void showData()
        {
            Console.WriteLine("userId=" + userId + ", name=" + name + ", mail=" + mail + ", password=" + password + ", tel=" + tel + ", address" + address);
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

            //入力チェック
            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext){
                if(name ==null){
                    yield return new ValidationResult ("名前は必須項目");
                }
                if(mail == null){
                    yield return new ValidationResult ("メールアドレスは必須項目");
                }else{
                    if(mail != null && !Regex.IsMatch(mail,"[a-zA-Z0-9.+-_%]+@[a-zA-Z0-9.-]+")){
                        yield return new ValidationResult ("メールアドレスは形式に従ってください");
                    }
                }
               
                if(password ==null){
                    yield return new ValidationResult ("パスワードは必須項目");
                }else{
                    if(!Regex.IsMatch(password,"[a-zA-Z0-9]")){
                        yield return new ValidationResult ("パスワードは半角英数字で入力してください");
                    }
                }
                
                if(tel == null){
                    yield return new ValidationResult ("電話番号は必須項目");
                }else{
                    if(!Regex.IsMatch(tel,"^0[789]0-[0-9]{4}-[0-9]{4}$")){
                        yield return new ValidationResult ("電話番号は形式に従ってください");
                    }
                }
                if(address == null){
                    yield return new ValidationResult ("住所は必須項目");
                }
            }
    }
}