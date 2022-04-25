using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website_BanHang.Context;
using Website_BanHang.Models;

namespace Website_BanHang.Controllers
{
    public class CategoryController : Controller
    {
        Website_BanHangEntities objWebASP = new Website_BanHangEntities();
        // GET: Category
        public ActionResult listCategory()
        {
            CategoryModel objCategoryModel = new CategoryModel();
            var lstCategory = objWebASP.Categories.ToList();
            objCategoryModel.lstCatgories = lstCategory;
            return View(objCategoryModel);
        }
       
        public ActionResult ProductCategoryByID(int id)
        {
            CategoryModel objCategoryModel = new CategoryModel();
            var lstProductCategoryByID = objWebASP.Products.Where(n => n.categoryID == id).ToList();
            objCategoryModel.lstProduct = lstProductCategoryByID;
            return View(objCategoryModel);
        }
    }
}