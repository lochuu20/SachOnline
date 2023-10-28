﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using PagedList;
using PagedList.Mvc;
using SachOnline.Models;

namespace SachOnline.Controllers
{
    public class HomeController : Controller
    { DataClasses1DataContext dbo = new DataClasses1DataContext("Data Source=FISHDABEZT\\FISHDABEZT;Initial Catalog=SachOnline;Integrated Security=True");
        private object db;

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login () { 
            return View(); 
        }
        [HttpPost]
        public ActionResult Login (FormCollection f) {
           // Gán các giá trị người dùng nhập liệu cho các biến
            var sTenDN = f ["UserName"];
            var sMatKhau = f ["Password"];
            //Gán giá trị cho đối tượng được tạo mới (ad)
            ADMIN ad = dbo.ADMINs.SingleOrDefault(n => n.TenDN == sTenDN && n.MatKhau
            == sMatKhau);
            if (ad != null)
            {
                Session["Admin"] = ad;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View(); }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}