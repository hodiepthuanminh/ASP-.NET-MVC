using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Website_BanHang.Context;
using Website_BanHang.Models;

namespace Website_BanHang.Controllers
{
    public class HomeController : Controller
    {
        Website_BanHangEntities objWebASP = new Website_BanHangEntities();
        public ActionResult Index()
        {
            HomeModel objHomeModel = new HomeModel();
            var lstCategory = objWebASP.Categories.ToList();
            var lstProductType = objWebASP.Products.Where(n => n.typeID == 1).ToList();
            objHomeModel.lstCategories = lstCategory;
            objHomeModel.lstProducts = lstProductType;
            return View(objHomeModel);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
       [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = objWebASP.Users.FirstOrDefault(s => s.username == _user.username);
                if (check == null)
                {
                    _user.password = GetMD5(_user.password);
                    objWebASP.Configuration.ValidateOnSaveEnabled = false;
                    objWebASP.Users.Add(_user);
                    objWebASP.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


            }
            return View("Index");


        }
        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(password);
                var data = objWebASP.Users.Where(s => s.username.Equals(username) && s.password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().firstname + " " + data.FirstOrDefault().lastname;
                    Session["username"] = data.FirstOrDefault().username;
                    Session["id"] = data.FirstOrDefault().id;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }



    }
}