﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingMvcApp.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.AspNetCore.Http;

// using Microsoft.AspNetCore.Authorization;

namespace ShoppingMvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ShoppingMvcAppContext _context;

        public HomeController(ILogger<HomeController> logger,ShoppingMvcAppContext context)
        {
            _logger = logger;
            _context = context;
           // User user = new User();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        // [Authorize]
        public IActionResult Form(string mail,string pass)
        {
            ViewData["Mail"] = Request.Form["mail"];
            string str_mail = Request.Form["mail"];
            ViewData["Password"] = Request.Form["pass"];
            string str_pass = Request.Form["pass"];

            var db_data  = _context.User.Where(user => user.mail == str_mail && user.password == str_pass).ToArray();


           // User userTmp = new User(mail,pass);
            User user = new User(mail,pass);
            HttpContext.Session.Set("object", user.ObjectToBytes(user));
            user = (User)user.ct(HttpContext.Session.Get("object"));
             //User user = new User(user.name,mail,pass,user.tel,user.address);
             ViewData["mail"] = user.mail;
             ViewData["pass"] =  user.password;
             ViewData["name"] = user.name;
             ViewData["tel"] = user.tel;
             ViewData["address"] = user.address;
             
                Console.WriteLine(user.mail);
                Console.WriteLine(user.password);
                Console.WriteLine(user.name);
                Console.WriteLine(user.tel);
                Console.WriteLine(user.address);

                
             Console.WriteLine("------------------------------------------");

             foreach (var item in db_data)
             {
                 Console.WriteLine(item.mail);
                 Console.WriteLine(item.password);
                 Console.WriteLine(item.address);
                 Console.WriteLine(item.tel);
                 Console.WriteLine(item.name);
             }

             if(db_data.Length ==1){
                Console.WriteLine("ログイン成功");
                return View("../Products/Index", _context.Product);
             }else{
                 Console.WriteLine("ログイン失敗");
             }
             
            return View("Index");
        }  
        public void LogOut(){
            if(HttpContext.Session.Get("object") ==null){

            }else{
                //HttpContext.Session.Get("object") =null;
                 //HttpContext.Session.Get("object").Clear;
            }
             User user = new User();
            if(ViewData["Message"] ==""){

            }
        }         
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
