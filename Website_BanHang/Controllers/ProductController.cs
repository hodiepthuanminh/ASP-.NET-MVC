using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website_BanHang.Context;

namespace Website_BanHang.Controllers
{
    public class ProductController : Controller
    {
        Website_BanHangEntities objWebASP = new Website_BanHangEntities();
        // GET: Product
        public ActionResult Detail(int id)
        {
            var objProduct = objWebASP.Products.Where(n=>n.id==id).FirstOrDefault();
            return View(objProduct);
        }
    }
}