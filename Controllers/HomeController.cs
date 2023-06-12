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

            var db_data  = _context.User.Where(user => user.mail == str_mail).ToArray();
            var db_pass  = _context.User.Where(user => user.password == str_pass).ToArray();

           // HttpContext.Session.SetString("Mail",mail);
           // HttpContext.Session.SetString("Pass",pass);
             User user = new User(mail,pass);
            Console.WriteLine(mail);
            Console.WriteLine(pass);
            HttpContext.Session.Set("object", user.ObjectToBytes(user));

             User user2 = (User)user.ct(HttpContext.Session.Get("object"));
            // ViewData["mail"] = ob2.GetMail();
            // ViewData["pass"] = ob2.GetPass();
    
   
            foreach (var item in db_data)
            {
                if(item.mail == str_mail){
                    foreach(var item2 in db_pass){
                        if(item2.password == str_pass){
                            Console.WriteLine("ログイン成功");
                            return View("../Products/Index", _context.Product);
                        }else{
                            Console.WriteLine("パスワードが違います。");
                            Console.WriteLine("ログイン失敗");
                             return View("Index");
                        }
                    }
                   
                }else{
                    Console.WriteLine("ログインIDがありません。");
                    Console.WriteLine("ログイン失敗");
                    return View("Index");
                }
            }
             
            return View("Index");
        }           
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
