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
using Microsoft.EntityFrameworkCore;

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
            
            User userTmp = new User();
            this.GetDbData(db_data,userTmp);

            User user = new User(userTmp.userId, userTmp.name,userTmp.mail,userTmp.password,userTmp.tel,userTmp.address);           

            HttpContext.Session.Set("object", user.ObjectToBytes(user));
            user = (User)user.ct(HttpContext.Session.Get("object"));

            //Console.WriteLine(ViewData["tel"]);

            if(db_data.Length ==1){
                Console.WriteLine("ログイン成功");
                ViewData["LoginResult"] = "ログイン成功";
                return View("../Products/Index", _context.Product);
            }else{
                Console.WriteLine("ログイン失敗");
                ViewData["LoginResult"] = "ログイン失敗";
            }
             
            return View("Index");
        }  
        private void GetDbData(User[] db_data,User user){
            foreach (var item in db_data)
             {
                user.userId = item.userId;
                user.mail = item.mail;
                ViewData["mail"] = user.mail;
                user.password = item.password;
                ViewData["pass"] = user.password;
                user.name = item.name;
                ViewData["name"] = user.name;
                user.tel = item.tel;
                ViewData["tel"] = user.tel;
                user.address = item.address;
                ViewData["address"] = user.address;
            }
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
