using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website_BanHang.Context;
using static Website_BanHang.Common;

namespace Website_BanHang.Areas.Admin.Controllers
{
    public class CategoryADController : Controller
    {
        Website_BanHangEntities objWebASP = new Website_BanHangEntities();
        // GET: Admin/CategoryAD
        public ActionResult Index()
        {
            var lstCategory = objWebASP.Categories.ToList();
            return View(lstCategory);
        }
        [HttpGet]
        public ActionResult Create()
        {
            this.LoadData();
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Category objCategory)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    if (objCategory.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                        string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                        fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyyMMddhhmmss")) + extension;
                        objCategory.avatar = fileName;
                        objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/product/"), fileName));
                        objWebASP.Categories.Add(objCategory);
                        objWebASP.SaveChanges();
                        return RedirectToAction("Index");
                    }

                }
                catch (Exception)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(objCategory);

        }
        [HttpGet]
        public ActionResult Detail(int id)
        {
            var objCategory = objWebASP.Categories.Where(n => n.id == id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objCategory = objWebASP.Categories.Where(n => n.id == id).FirstOrDefault();
            return View(objCategory);
        }

        [HttpPost]
        public ActionResult Edit(Category objCategory)
        {
            if (objCategory.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyyMMddhhmmss")) + extension;
                objCategory.avatar = fileName;
                objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/product/"), fileName));

            }
            objWebASP.Entry(objCategory).State = EntityState.Modified;
            objWebASP.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objCategory = objWebASP.Categories.Where(n => n.id == id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpPost]
        public ActionResult Delete(Category objCate)
        {
            var objCategory = objWebASP.Categories.Where(n => n.id == objCate.id).FirstOrDefault();
            objWebASP.Categories.Remove(objCategory);
            objWebASP.SaveChanges();
            return RedirectToAction("Index");
        }

        void LoadData()
        {
            Common objCommon = new Common();
            var lstPop = objWebASP.Categories.ToList();
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            

            
            List<CategoryPopular> lstCategoryPopular = new List<CategoryPopular>();
            CategoryPopular objCategoryPopular = new CategoryPopular();
            objCategoryPopular.Id = 1;
            objCategoryPopular.Name = "Phổ biến";
            lstCategoryPopular.Add(objCategoryPopular);

            objCategoryPopular = new CategoryPopular();
            objCategoryPopular.Id = 0;
            objCategoryPopular.Name = "Không phổ biến";
            lstCategoryPopular.Add(objCategoryPopular);
            DataTable dtCategoryPopular = converter.toDataTable(lstCategoryPopular);
            ViewBag.CategoryPopular = objCommon.ToSelectList(dtCategoryPopular, "Id", "Name");
        }

    }
}